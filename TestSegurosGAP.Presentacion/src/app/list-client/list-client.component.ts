import { Component, OnInit , Inject} from '@angular/core';
import {Router} from "@angular/router";
import {Client} from "../model/client.model";
import {ApiService} from "../core/api.service";
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-list-client',
  templateUrl: './list-client.component.html',
  styleUrls: ['./list-client.component.css']
})
export class ListClientComponent implements OnInit {

  clients: Client[];

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    if(!window.localStorage.getItem('token')) {
      this.router.navigate(['login']);
      return;
    }
    this.apiService.getClients()
        .subscribe(data => {
            this.clients = data.result;
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
  }

    viewInsurancesClient(client: Client): void {
        this.router.navigate(['list-insurance'], { queryParams: { clientId: client.IdCliente.toString() } });
    }

  editClient(client: Client): void {
    window.localStorage.removeItem("editClientId");
    window.localStorage.setItem("editClientId", client.IdCliente.toString());
    this.router.navigate(['edit-client']);
  };

  addClient(): void {
    this.router.navigate(['add-client']);
  };
}
