import { Component, OnInit } from '@angular/core';
import { HistoryService } from '../packXPrez-services/history.service';

@Component({
  selector: 'app-track-order',
  templateUrl: './track-order.component.html',
  styleUrls: ['./track-order.component.css']
})
export class TrackOrderComponent implements OnInit {
  userRole: string;
  customerLayout: boolean = false;
  commonLayout: boolean = false;
  status: string;
  message: string
  errMsg: string;
  gif: boolean;
  constructor(private _historyService: HistoryService) {
    this.userRole = sessionStorage.getItem('userRole');
    if (this.userRole == "Customer") {
      this.customerLayout = true;
    }
    else {
      this.commonLayout = true;
    }
  }

  ngOnInit() {
  }
  getStatus(num: number) {
    this.gif = true;
    this._historyService.getStatus(num).subscribe(responseStatus => {
      this.gif = false;
      this.status = responseStatus;
      if (this.status == "") { this.message = "Invalid  AWB Number"; }
      else if (this.status == null) { this.message = "Invalid  AWB Number"; }
      else { this.message = "Your order is " + this.status; }
      },
      responseProductError => {
        this.status = null;
        this.errMsg = responseProductError;
        console.log(this.errMsg);
      }, () => { console.log(this.status) })
  }
}
