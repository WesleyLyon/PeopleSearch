import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../models/person';
import { Observable } from 'rxjs/Observable';

@Injectable()

export class WebapiService {
  public baseUrl: string = window.location.origin;
  public fullUrl: string;
  public persons: Person[];

  constructor(private httpClient: HttpClient) { }

  getPeople(data: { searchString: string }): Observable<any> {
    this.fullUrl = this.baseUrl + '/api/People/GetPersons';
    console.log(this.fullUrl);
    return this.httpClient.post(this.fullUrl, data);
  }
}
