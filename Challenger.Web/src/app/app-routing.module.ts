import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FitRecordsComponent } from './components/fit-records/fit-records.component';
import { GymRecordsComponent } from './components/gym-records/gym-records.component';
import { HomeComponent } from './components/home/home.component';
import { MeasurementsComponent } from './components/measurements/measurements.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'measurements', component: MeasurementsComponent },
  { path: 'gymRecords', component: GymRecordsComponent },
  { path: 'fitRecords', component: FitRecordsComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
