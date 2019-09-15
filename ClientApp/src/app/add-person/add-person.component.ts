import { Component, Inject } from '@angular/core';
import { Person } from '../models/person';
import { WebapiService } from '../services/webapi.service';

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html',
})

export class AddPersonComponent {
  public persons: Person[];
  isSearching: boolean = false;

  constructor(private webApiService: WebapiService) {
    this.searchPeople('');
  };

  public searchPeople(searchString) {
    this.isSearching = true;

    this.webApiService.getPeople({ searchString: searchString }).subscribe(result => {
      this.persons = result;
      this.isSearching = false;
    });
  }
};
