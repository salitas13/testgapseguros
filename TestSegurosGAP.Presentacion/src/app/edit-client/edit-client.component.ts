import { Component, OnInit , Inject} from '@angular/core';
import {Router} from "@angular/router";
import {Client} from "../model/client.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {first} from "rxjs/operators";
import {ApiService} from "../core/api.service";
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-client',
  templateUrl: './edit-client.component.html',
  styleUrls: ['./edit-client.component.css']
})
export class EditClientComponent implements OnInit {

  client: Client;
  editForm: FormGroup;
  constructor(private formBuilder: FormBuilder,private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    let clientId = window.localStorage.getItem("editClientId");
    if(!clientId) {
      alert("Invalid action.")
      this.router.navigate(['list-client']);
      return;
    }
    this.editForm = this.formBuilder.group({
      IdCliente: [''],
      Nombres: ['', Validators.required],
      Apellidos: ['', Validators.required],
      FechaNacimiento: ['', Validators.required],
      Cedula: ['', Validators.required]
    });

    this.apiService.getClientById(+clientId)
      .subscribe( data => {
        this.editForm.setValue(data.result);
      });
  }

  onSubmit() {
    this.apiService.updateClient(this.editForm.value)
      .pipe(first())
      .subscribe(
        data => {
          if(data.status === 200) {
            alert('Cliente actualizado satisfatoriamente.');
            this.router.navigate(['list-client']);
          }else {
            alert(data.message);
          }
        },
        error => {
            if (error instanceof HttpErrorResponse) {
                switch (error.status) {
                    case 0:      //login
                        window.localStorage.removeItem("token");
                        window.localStorage.removeItem("editClientId");
                        this.router.navigate(['login']);
                        break;
                    case 401:      //login
                        window.localStorage.removeItem("token");
                        window.localStorage.removeItem("editClientId");
                        this.router.navigate(['login']);
                        break;
                    case 403:     //
                        window.localStorage.removeItem("token");
                        window.localStorage.removeItem("editClientId");
                        this.router.navigate(['login']);
                        break;
                }
            }
        });
  }

}
