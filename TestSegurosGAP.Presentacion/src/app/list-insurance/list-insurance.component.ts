import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import {Insurance} from "../model/insurance.model";
import {ApiService} from "../core/api.service";
import { HttpErrorResponse } from '@angular/common/http';

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

  editEnsurance(insurance: Insurance): void {
    window.localStorage.removeItem("editInsuranceId");
    window.localStorage.setItem("editInsuranceId", insurance.IdPoliza.toString());
    this.router.navigate(['edit-insurance']);
  };

    addInsurance(): void {
        this.router.navigate(['add-insurance'], { queryParams: { clientId: this.clientId.toString() } });
    };
}

