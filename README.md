# Cheap Movies

Cheap Movies is a web app built with .Net Core and Angular. It is inspired by the front page of Booko.com.au, which lists the featured books and the prices/retailers list in a tiled layout.

A demo of this app can be found at [http://cheapmovies.azurewebsites.net](http://cheapmovies.azurewebsites.net).

## Assumptions

- Starting with 2 data providers (cinemaworld and filmworld), it may expand to have more service providers.
- The 2 data providers are currently on the same server, and using the same security credentials, but can have different URL's and credentials in the future.
- The providers may return errors, but, from experience, usually send the correct output given enough retries, most of the time.
- The providers use a common movie ID format XX0000000 where XX is the two-character provider code, and 0000000 is the common movie ID shared by them.

## Architecture

Designed to eventually evolve to a bigger application, the app is organised into multiple projects.
- CheapMovies.Api: Contains a thin controller that calls the service layer. Currently, it set up by the same application builder as the SPA. It can have its own application builder startup when moving towards a microservice setup.
- CheapMovies.Common: For utilities and constants. It currently contains the following:
  - RetryHelper: A simple retry utility for retrying an operation when it fails. For production, it can be replaced by a more robust solution like Polly.
  - Constants
- CheapMovies.Domain: For entities like movie.
- CheapMovies.Store: A simple key-value data store solution that uses sqlite. May be replaced by a more performant service like Redis.
- CheapMovies.Tests: xUnit tests
- CheapMovies.Web: Contains the SPA, built with Angular. It also sets up the server that hosts the API and services.

## Configuration

The following options can be configured in appsettings.json
- Retries (max retry attempts, seconds pause between failures, timeout in seconds)
- Data providers (name, base address, movies service, movie service, token, prefix). If the token needs to be secured, consider using user-secrets.

## Handling Service Failures

The following steps are used to deal with providers' web service failures:
- If successful, store a copy of the returned data.
- If failed, retry X times, waiting for Y seconds between failures. (X and Y are configured in appsettings.json.)
- If it still fails after X times, retrieve the stored copy, with a tag as coming "from store".

## Unit Testing

The unit test covers the movie service logic that retrieves from the local store if the web service fails.
