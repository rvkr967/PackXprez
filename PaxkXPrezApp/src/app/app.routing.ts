import { RouterModule, Routes } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { TrackOrderComponent } from './track-order/track-order.component';
import { LoginComponent } from './login/login.component';
import { CustomerLayoutComponent } from './layouts/customer-layout/customer-layout.component';
import { ViewPackageHistoryComponent } from './view-package-history/view-package-history.component';
import { RegisterShipmentComponent } from './register-shipment/register-shipment.component';
import { UserRegistrationComponent } from './user-registration/user-registration.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'trackorder', component: TrackOrderComponent },
  { path: 'login', component: LoginComponent },
  { path: 'ctrlayout', component: CustomerLayoutComponent },
  { path: 'packagehistory', component: ViewPackageHistoryComponent },
  { path: 'shipmentregistration', component: RegisterShipmentComponent },
  { path: 'userregistration', component: UserRegistrationComponent }
  
];
export const routing: ModuleWithProviders = RouterModule.forRoot(routes);
