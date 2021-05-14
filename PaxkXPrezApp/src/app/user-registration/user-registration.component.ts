import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IUserReg } from '../packXPrez-interfaces/userReg';
import { UserRegService } from '../packXPrez-services/user-registration/user-reg.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit {
  p: string;
  cp: string;
  pin: number;
  gif: boolean = false;
  regStatus: number;
  failStatus: boolean = false;
  constructor(private _userregService: UserRegService, private router: Router) { }

  ngOnInit() {
  }
  submituserRegForm(form: NgForm) {
    this.gif = true;
    console.log(form.value.pincode)
    let obj: IUserReg = {
      name: form.value.name,
      emailId: form.value.email,
      password: form.value.password,
      mobile: form.value.mobile,
      buildingNo: form.value.bdno,
      streetNo: form.value.stno,
      locality: form.value.locality,
      pincode: form.value.pincode
    }
    this._userregService.userRegistration(obj).subscribe(
      responseData => {
        this.gif = false;
        this.regStatus = responseData;
        if (responseData==1) {
          sessionStorage.setItem('userName', form.value.email);
          sessionStorage.setItem('userRole', "Customer");
          this.router.navigate(['/home'])
        }
        else {
          this.failStatus = true;
        }
      }, responseLoginError => {
        console.log(responseLoginError);
      },
      () => console.log("SubmitRegForm method executed successfully"))
  }

}
