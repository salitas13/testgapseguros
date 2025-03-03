import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {ApiService} from "../core/api.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  invalidLogin: boolean = false;
  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: ApiService) { }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    const loginPayload = {
      username: this.loginForm.controls.username.value,
      password: this.loginForm.controls.password.value
    }
    this.apiService.login(loginPayload).subscribe(data => {
      if(data.status === 200) {
        window.localStorage.setItem('token', data.result);
        this.router.navigate(['list-client']);
      }else {
        this.invalidLogin = true;
        //alert(data.message);
      }
    });
  }

    ngOnInit() {

        this.loginForm = this.formBuilder.group({
          username: ['', Validators.compose([Validators.required])],
          password: ['', Validators.required]
        });

        if (window.localStorage.getItem('token')) {
            this.router.navigate(['list-client']);
            return;
        }

        window.localStorage.removeItem('token');
      }
}