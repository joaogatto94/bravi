import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { PersonFormComponent } from '../person-form/person-form.component';
import { ContactListComponent } from '../contact-list/contact-list.component';
import { Person } from 'src/app/models/person';

@Component({
  selector: 'app-persons-list',
  standalone: true,
  imports: [CommonModule, PersonFormComponent, ContactListComponent],
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.scss']
})
export class PersonsListComponent {
  people: Person[] = [];

  selectedPerson: Person | null = null;
  showChild: `form` | `contact` | null = null;

  constructor(
    private apiService: ApiService
  ) { this.setPeople(); }

  async setPeople() {
    this.people = await this.apiService.getPersons();
  }

  openForm(person: any) {
    this.selectedPerson = person;
    this.showChild = `form`;
  }

  openContactList(person: any) {
    this.selectedPerson = person;
    this.showChild = `contact`;
  }

  personFormSaved() {
    this.setPeople();
    this.showChild = null;
  }

  async deletePerson(person: any) {
    await this.apiService.deletePerson(person.id);
    this.people = this.people.filter(p => p.id !== person.id);
  }
}
