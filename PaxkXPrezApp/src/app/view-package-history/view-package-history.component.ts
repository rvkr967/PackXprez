import { Component, OnInit } from '@angular/core';
import { IHistory } from '../packXPrez-interfaces/packagehistory';
import { HistoryService } from '../packXPrez-services/history.service';

@Component({
  selector: 'app-view-package-history',
  templateUrl: './view-package-history.component.html',
  styleUrls: ['./view-package-history.component.css']
})
export class ViewPackageHistoryComponent implements OnInit {
  history: IHistory[];
  errMsg: string;
  hist: IHistory[] = [{ transactionId: 555555, number: '123456789', fromLocation: 'kkbd', toLocation: 'tpt', status:'success' }]
  constructor(private _historyService: HistoryService) { }

  ngOnInit() {
    this.getHistory();
  }
  getHistory() {
    let email = sessionStorage.getItem('userName');
    this._historyService.getHistory(email).subscribe(responseHistory => { this.history = responseHistory },
      responseProductError => {
        this.history = null;
        this.errMsg = responseProductError;
        console.log(this.errMsg);
      });
  }

}
