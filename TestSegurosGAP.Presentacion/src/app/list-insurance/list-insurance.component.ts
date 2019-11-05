import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import {Insurance} from "../model/insurance.model";
import {ApiService} from "../core/api.service";
import { HttpErrorResponse } from '@angular/common/http';
import { first } from "rxjs/operators";

@Component({
  selector: 'app-list-poliza',
  templateUrl: './list-insurance.component.html',
  styleUrls: ['./list-insurance.component.css']
})
export class ListInsuranceComponent implements OnInit {

    insurances: Insurance[];
    sub: any;
    clientId: string;

    constructor(private router: Router, private activatedRoute: ActivatedRoute, private apiService: ApiService) { }

  ngOnInit() {
    if (!window.localStorage.getItem('token')) {
  
        this.router.navigate(['login']);
        return;
    }

      
      this.sub = this.activatedRoute.queryParamMap.subscribe(params => {
          this.clientId = params.get("clientId");

          if (!this.clientId) {
              alert("Invalid action.")
              this.router.navigate(['list-client']);
              return;
          }

          this.updateList();
        });
  }

    updateList(): void {
        this.apiService.getInsurancesClient(+this.clientId) // (+) converts string 'insuranceId' to a number
            .subscribe(data => {
                this.insurances = data.result;
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

    editInsurance(insurance: Insurance): void {
        this.router.navigate(['edit-insurance'], { queryParams: { insuranceId: insurance.IdPoliza.toString() } });
    };

    cancelInsurance(insurance: Insurance): void {
        insurance.Estado = false;
        this.apiService.updateInsurance(insurance)
            .pipe(first())
            .subscribe(
                data => {
                    if (data.status === 200) {
                        alert('Póliza cancelada satisfactoriamente.');
                        this.updateList();
                    } else {
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

    addInsurance(): void {
        this.router.navigate(['add-insurance'], { queryParams: { clientId: this.clientId.toString() } });
    };
}

