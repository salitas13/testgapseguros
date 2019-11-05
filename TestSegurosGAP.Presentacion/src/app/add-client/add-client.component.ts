import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {ApiService} from "../core/api.service";

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.css']
})
export class AddClientComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,private router: Router, private apiService: ApiService) { }

  addForm: FormGroup;

  ngOnInit() {
    this.addForm = this.formBuilder.group({
      IdCliente: [],
        Nombres: ['', [Validators.required, Validators.minLength(50)]],
        Apellidos: ['', [Validators.required, Validators.minLength(50)]],
        FechaNacimiento: ['', Validators.required],
        Cedula: ['', [Validators.required, Validators.minLength(10)]]
    });

  }

  onSubmit() {
    this.apiService.createClient(this.addForm.value)
      .subscribe( data => {
          if (data.status === 200) {
              alert('Cliente creado satisfatoriamente.');
              this.router.navigate(['list-client']);
          } else {
              alert(data.message);
          }
      });
  }

}
