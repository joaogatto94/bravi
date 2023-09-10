import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from 'src/app/services/api.service';
import { Person } from 'src/app/models/person';
import { Contact } from 'src/app/models/contact';
import { ContactFormComponent } from '../contact-form/contact-form.component';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [CommonModule, ContactFormComponent],
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.scss']
})
export class ContactListComponent {
  @Input() set person(person: Person | null) {
    if (person) {
      this._person = person;
      this.getContacts();
    }
  };

  _person: Person | undefined;

  contacts: Contact[] = [];
  selectedContact: Contact | null | undefined;
  showForm = false;

  constructor(
    private apiService: ApiService
  ) {}

  async getContacts() {
    this.contacts = await this.apiService.getContacts(this._person!.id);
  }

  openForm(contact: Contact | null) {
    this.selectedContact = contact;
    this.showForm = true;
  }

  contactSaved() {
    this.getContacts();
    this.showForm = false;
  }

  deleteContact(contact: Contact) {
    this.apiService.deleteContact(contact.id);
    this.contacts = this.contacts.filter(c => c.id !== contact.id);
  }
}
