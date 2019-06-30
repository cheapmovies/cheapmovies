import { Price } from './price';

export class Movie {
    title = '';
    year = 0;
    fullId = '';
    id = '';
    type = '';
    poster = '';
    prices: Price[] = new Array();
}
