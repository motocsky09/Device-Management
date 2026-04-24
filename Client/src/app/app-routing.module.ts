import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeviceListComponent } from './components/device-list/device-list.component';
import { DeviceDetailComponent } from './components/device-detail/device-detail.component';
import { DeviceFormComponent } from './components/device-form/device-form.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'devices', component: DeviceListComponent, canActivate: [AuthGuard] },
  { path: 'devices/new', component: DeviceFormComponent, canActivate: [AuthGuard] },
  { path: 'devices/edit/:id', component: DeviceFormComponent, canActivate: [AuthGuard] },
  { path: 'devices/:id', component: DeviceDetailComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }