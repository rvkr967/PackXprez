import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { RegisterShipmentService } from '../packXPrez-services/register-shipment/register-shipment.service';
import { IAddress } from '../packXPrez-interfaces/addresses';
import { IRegistration } from '../packXPrez-interfaces/registration';
import { from } from 'rxjs';

@Component({
  selector: 'app-register-shipment',
  templateUrl: './register-shipment.component.html',
  styleUrls: ['./register-shipment.component.css']
})
export class RegisterShipmentComponent implements OnInit {
  kms: number;
  valid2: boolean;
  valid1: boolean;
  addresses: IAddress[];
  tranId: number;
  errMsg: string;
  succes: boolean = false;
  price: number = 0;
  len: number;
  breadth: number;
  height: number;
  weight: number;
  shiptype: string;
  pkgserv: boolean;
  gif: boolean;
  gif1: boolean;
  succes1: boolean=false;
  constructor(private _registerService: RegisterShipmentService) { }
  ngOnInit() {
    this.getAddresses()
  }
  submitvalForm(form: NgForm) {
    this.gif = true;
    this._registerService.serviceValidation(form.value.pkpin, form.value.delpin).subscribe(
      responseVal => {
        this.gif = false;
        this.kms = responseVal;
        if (this.kms != 0) { this.valid2 = true; this.valid1 = false; }
        else { this.valid1 = true; this.valid2 = false; }
      },
      responseLoginError => {
        console.log(responseLoginError);
      },
      () => console.log("SubmitValidateForm method executed successfully"))
  }
  getAddresses() {
    let tempmail = sessionStorage.getItem('userName');
    this._registerService.getAddresses(tempmail).subscribe(
      responseAddresses => { this.addresses = responseAddresses; })
  }
  submitregForm(form: NgForm) {
    this.gif1 = true;
    var regObj: IRegistration;
    regObj = {
      emailId: sessionStorage.getItem('userName'),
      shippingType: form.value.shiptype,
      length: form.value.length,
      breadth: form.value.breadth,
      height: form.value.height,
      weight: form.value.weight,
      deliveryType: form.value.deloption,
      timeSlot: form.value.timeslot,
      pickAddressId: form.value.pkaddress,
      deliveryAddress: form.value.bdno + ',' + form.value.stno + ',' + form.value.lclty + '-' + form.value.rcvrpcode + '(Ph:' + form.value.mobile + ')',
      packingService: form.value.pksserv
    }
    this._registerService.shipmentRegistration(regObj).subscribe(
      responseTranId => {
        this.gif1 = false;
        if (responseTranId != -1) {
          this.tranId = responseTranId;
          this.succes = true;
        }
        else { this.succes1 = true; }
      },
      responseLoginError => {
        console.log(responseLoginError);
        this.errMsg = 'Something went Wrong.Please try again!!'
      },
      () => console.log("SubmitValidateForm method executed successfully"))
  }
  calculateCharges() {
    this.price = 200 + (this.kms*7);
    if (this.shiptype == "Express") { this.price += 100; }
    else if (this.shiptype == "Overnight") { this.price += 500; }
    if (this.pkgserv) { this.price += 500; }
    if (this.weight > 5) { this.price += this.weight * 50; }
    let vol = this.len * this.breadth * this.height;
    if (vol > 100) { this.price += vol * 50; }
  }

}
