import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Movie } from '../entities/movie';
import { Price } from '../entities/price';
import { Observable } from 'rxjs';

export class MovieService {

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string) {
  }

  public getProviders(): Observable<any> {
    return this.http.get(
      this.baseUrl + 'api/movie/getproviders'
    );
  }

  public getMovies(serviceId: number): Observable<any> {
    return this.http.get(
      this.baseUrl + 'api/movie/getmovies?serviceId=' + serviceId
    );
  }

  public setMoviePrices(providers: string[], movie: Movie) {
    movie.prices = new Array();
    for (let i = 0; i < providers.length; i++) {
      const provider = providers[i];
      this.getPrice(i, movie.id)
      .subscribe((result: any) => {
        if (result) {
          const price = new Price();
          price.name = provider;
          price.price = result.price;
          price.fromStore = result.fromStore;
          movie.prices.push(price);
          movie.prices = movie.prices.sort((a, b) => a.price - b.price);
        }
      });
    }
  }

  public getPrice(serviceId: number, movieId: string): Observable<Price> {
    return this.http.get<Price>(
      this.baseUrl + 'api/movie/getmovie?serviceId=' + serviceId + '&movieId=' + movieId
    );
  }
}
