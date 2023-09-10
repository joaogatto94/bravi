import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-person-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.scss']
})
export class PersonFormComponent {
  @Input() set person(person: any) {
    this.personId = person?.id;
    this.form.get(`name`)?.setValue(person?.name || null);
  };
  @Output() personSaved = new EventEmitter();
  @Output() cancel = new EventEmitter();

  form = this.fb.group({
    name: [null, Validators.required]
  });
  personId = null;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService
    ) {}

  async save() {
    if (!this.form.valid) {
      alert(`Formulario invalido`);
    }

    if (this.personId) {
      await this.apiService.updatePerson(this.personId, this.form.get(`name`)!.value!);
    } else {
      await this.apiService.createPerson(this.form.get(`name`)!.value!);
    }
    this.personSaved.emit();
  }
}
