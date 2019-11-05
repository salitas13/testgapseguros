import { Component, OnInit , Inject} from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {first} from "rxjs/operators";
import {ApiService} from "../core/api.service";
import { HttpErrorResponse } from '@angular/common/http';
import { Insurance } from '../model/insurance.model';
import { TypeCovering } from '../model/typecovering.model';
import { TypeRisk } from '../model/typerisk.model';

@Component({
  selector: 'app-edit-client',
  templateUrl: './edit-insurance.component.html',
    styleUrls: ['./edit-insurance.component.css']
})
export class EditInsuranceComponent implements OnInit {

    typescovering: TypeCovering[];
    typesrisk: TypeRisk[];
    insurance: Insurance;
    editForm: FormGroup;
    sub: any;
    insuranceId: string;


    constructor(private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute, private apiService: ApiService) { }

    ngOnInit() {
        this.editForm = this.formBuilder.group({
            IdPoliza: [],
            IdCliente: [],
            Nombre: [null, Validators.required],
            Descripcion: [null, Validators.required],
            FechaInicioVigencia: [null, Validators.required],
            PeriodoCobertura: [null, Validators.required],
            PrecioPoliza: [null, Validators.required],
            IdTipoCubrimiento: [null, Validators.required],
            IdTipoRiesgo: [null, Validators.required],
            Cobertura: [null, Validators.required]
        });

      this.sub = this.activatedRoute.queryParamMap.subscribe(params => {
          this.insuranceId = params.get("insuranceId");

          if (!this.insuranceId) {
              alert("Invalid action.")
              this.router.navigate(['list-insurance']);
              return;
          }

          this.apiService.getTypesCovering()
              .subscribe(data => {
                  this.typescovering = data.result;

                  this.apiService.getInsuranceById(+this.insuranceId)
                      .subscribe(data => {
                          this.editForm.setValue(data.result);
                      });

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

          this.apiService.getTypesRisk()
              .subscribe(data => {
                  this.typesrisk = data.result;

                  this.apiService.getInsuranceById(+this.insuranceId)
                      .subscribe(data => {
                          this.editForm.setValue(data.result);
                      });

              },
            error => {
                if (error instanceof HttpErrorResponse) {
                    switch (error.status) {
                        case 0:      //login
                            window.localStorage.removeItem("token");
                            window.localStorage.removeItem("editClientId");
                            window.localStorage.removeItem("editInsuranceId");
                            this.router.navigate(['login']);
                            break;
                        case 401:      //login
                            window.localStorage.removeItem("token");
                            window.localStorage.removeItem("editClientId");
                            window.localStorage.removeItem("editInsuranceId");
                            this.router.navigate(['login']);
                            break;
                        case 403:     //
                            window.localStorage.removeItem("token");
                            window.localStorage.removeItem("editClientId");
                            window.localStorage.removeItem("editInsuranceId");
                            this.router.navigate(['login']);
                            break;
                    }
                }
            });
      });
  }

  onSubmit() {
    this.apiService.updateInsurance(this.editForm.value)
      .pipe(first())
      .subscribe(
        data => {
          if(data.status === 200) {
            alert('Póliza actualizado satisfatoriamente.');
            this.router.navigate(['list-insurance']);
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
