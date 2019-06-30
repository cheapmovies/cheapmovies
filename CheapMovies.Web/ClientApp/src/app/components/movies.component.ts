import { Component, OnInit } from '@angular/core';
import _ from 'lodash';
import { MovieService } from '../services/movie.service';
import { Movie } from '../entities/movie';
import { Price } from '../entities/price';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
})
export class MoviesComponent implements OnInit {
  public moviesLoaded = false;
  public providers: string[];
  public movies: Movie[] = new Array();

  constructor(
    private movieService: MovieService
  ) {}

  public ngOnInit() {
    this.movieService.getProviders()
      .subscribe(result => {
        this.providers = result;
      });
    this.movieService.getMovies(0)
      .subscribe(result => {
        this.movies = result;
        this.orderBy('title', 'asc');
        this.moviesLoaded = true;
        this.populatePrices();
      });
  }

  private populatePrices() {
    this.movies.forEach(movie => {
      this.movieService.setMoviePrices(this.providers, movie);
    });
  }

  public orderBy(field: string, order: string) {
    this.movies = _.orderBy(this.movies, field, order);
  }
}
