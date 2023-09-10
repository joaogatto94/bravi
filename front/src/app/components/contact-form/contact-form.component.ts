import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Contact } from 'src/app/models/contact';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-contact-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss']
})
export class ContactFormComponent {
  @Input() set contact(model: Contact | null | undefined) {
    this.contactId = model?.id;
    this.form.patchValue({
      name: model?.name,
      phone: model?.phone,
      whatsapp: model?.whatsapp,
      email: model?.email,
    });
  };

  @Input() personId: number | undefined;

  @Output() contactSaved = new EventEmitter();
  @Output() cancel = new EventEmitter();

  contactId: number | undefined | null;

  form = this.fb.group({
    name: [``, Validators.required],
    phone: [``],
    whatsapp: [``],
    email: [``, Validators.email]
  })

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService
  ) {
  }

  async save() {
    if (!this.form.valid) {
      alert(`formulario invalido!`);
    }

    if (this.contactId) {
      await this.apiService.updateContact(this.contactId, this.form.value);
    } else {
      await this.apiService.createContact({...this.form.value, ...{personId: this.personId}})
    }

    this.contactSaved.emit();
  }
}
