import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Person } from '../models/person';
import { Contact } from '../models/contact';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiUrl = 'https://localhost:7016';

  constructor(
    private httpClient: HttpClient
  ) { }

  getPersons(): Promise<Person[]> {
    return firstValueFrom(this.httpClient.get(`${this.apiUrl}/persons`)) as any;
  }

  getContacts(personId: number): Promise<Contact[]> {
    return firstValueFrom(this.httpClient.get(`${this.apiUrl}/persons/${personId}/contacts`)) as any;
  }

  createPerson(name: string): Promise<any> {
    return firstValueFrom(this.httpClient.post(`${this.apiUrl}/persons`, {name})) as any;
  }

  updatePerson(id: number, name: string): Promise<any> {
    return firstValueFrom(this.httpClient.put(`${this.apiUrl}/persons/${id}`, {name})) as any;
  }

  deletePerson(id: number): Promise<any> {
    return firstValueFrom(this.httpClient.delete(`${this.apiUrl}/persons/${id}`)) as any;
  }

  createContact(body: any): Promise<any> {
    return firstValueFrom(this.httpClient.post(`${this.apiUrl}/contacts`, body)) as any;
  }

  updateContact(contactId: number, body: any): Promise<any> {
    return firstValueFrom(this.httpClient.put(`${this.apiUrl}/contacts/${contactId}`, body)) as any;
  }

  deleteContact(contactId: number): Promise<any> {
    return firstValueFrom(this.httpClient.delete(`${this.apiUrl}/contacts/${contactId}`)) as any;
  }
}
