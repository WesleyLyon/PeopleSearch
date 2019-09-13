import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../models/person';

//@Injectable({
//  providedIn: 'root'
//})

export class WebApiService {
  public baseUrl: string = window.location.origin;
  public persons: Person[];

  constructor(private httpClient: HttpClient) {

  }

  public getPeople() {
      return this.httpClient.get<Person[]>(this.baseUrl + 'api/SampleData/GetPeople').subscribe(result => {
        this.persons = result;
      }, error => console.error(error));
  }
}
