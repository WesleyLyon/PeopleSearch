import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../models/person';
import { WebapiService } from '../services/webapi.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  public persons: Person[];

  constructor(private webApiService: WebapiService) {
    this.searchPeople('');
  };

  public searchPeople(searchString) {
    this.webApiService.getPeople({ searchString: searchString }).subscribe(result => this.persons = result);
  }
};
