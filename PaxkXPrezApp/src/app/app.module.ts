import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CommonLayoutComponent } from './layouts/common-layout/common-layout.component';
import { TrackOrderComponent } from './track-order/track-order.component';
import { routing } from './app.routing';
import { LoginComponent } from './login/login.component';
import { CustomerLayoutComponent } from './layouts/customer-layout/customer-layout.component';
import { HttpClientModule } from '@angular/common/http';
import { ViewPackageHistoryComponent } from './view-package-history/view-package-history.component';
import { HistoryService } from './packXPrez-services/history.service';
import { RegisterShipmentComponent } from './register-shipment/register-shipment.component';
import { RegisterShipmentService } from './packXPrez-services/register-shipment/register-shipment.service';
import { UserRegistrationComponent } from './user-registration/user-registration.component';

@NgModule({
  declarations: [
    AppComponent, HomeComponent, CommonLayoutComponent, TrackOrderComponent, LoginComponent,
    CustomerLayoutComponent, ViewPackageHistoryComponent, RegisterShipmentComponent, UserRegistrationComponent
  ],
  imports: [
    BrowserModule, routing, HttpClientModule,FormsModule
  ],
  providers: [HistoryService, RegisterShipmentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
