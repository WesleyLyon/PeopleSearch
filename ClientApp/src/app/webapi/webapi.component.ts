import { Component, OnInit, Injectable } from '@angular/core';
import { WebapiService } from '../services/webapi.service';
import { Person } from '../models/person';

@Injectable()

@Component({
  selector: 'app-webapi',
  templateUrl: './webapi.component.html',
  providers: [WebapiService]
})

export class WebApiComponent {
  people: Person[];

  constructor(private WebapiService: WebapiService) {
  }

  //ngOnInit() {
  //  return this.getPeople('');
  //}

  getPeople(searchString) {
    //console.log('test');
    //console.log(searchString);
    this.WebapiService.getPeople({ searchString: searchString }).subscribe(result => { this.people = result });
    console.log(this.people)
    return this.people;
  }
}
