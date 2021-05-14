import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { RegisterShipmentService } from '../packXPrez-services/register-shipment/register-shipment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  status: string;
  errorMsg: string;
  msg: string;
  showDiv: boolean = false;
  gif: boolean;

  constructor(private _registerService: RegisterShipmentService, private router: Router) { }
  submitLoginForm(form: NgForm) {
    this.gif = true;
    this._registerService.validateUserr(form.value.email, form.value.password).subscribe(
      responseLoginStatus => {
        this.gif = false;
        this.status = responseLoginStatus;
        console.log(this.status)
        this.showDiv = true;
        if (this.status.toLowerCase() != "invalid credentials") {
          this.msg = "Login Successful";
          sessionStorage.setItem('userName', form.value.email);
          sessionStorage.setItem('userRole', this.status);
          this.router.navigate(['/home'])
        }
        else {
          this.msg = this.status + ". Try again with valid credentials.";
        }
      },
      responseLoginError => {
        this.errorMsg = responseLoginError;
      },
      () => console.log("SubmitLoginForm method executed successfully")
    );
  }

  ngOnInit() {
  }

}
