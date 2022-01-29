import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MeasurementItemComponent } from './components/measurement-item/measurement-item.component';
import { AddMeasurementComponent } from './components/add-measurement/add-measurement.component';

import { FormsModule } from '@angular/forms';
import { MeasurementsComponent } from './components/measurements/measurements.component';
import { GymRecordItemComponent } from './components/gym-record-item/gym-record-item.component';
import { FitRecordItemComponent } from './components/fit-record-item/fit-record-item.component';
import { GymRecordsComponent } from './components/gym-records/gym-records.component';
import { AddGymRecordComponent } from './components/add-gym-record/add-gym-record.component';
import { AddFitRecordComponent } from './components/add-fit-record/add-fit-record.component';
import { FitRecordsComponent } from './components/fit-records/fit-records.component';
import { ButtonComponent } from './components/button/button.component';

@NgModule({
  declarations: [
    AppComponent,
    MeasurementItemComponent,
    AddMeasurementComponent,
    MeasurementsComponent,
    GymRecordItemComponent,
    FitRecordItemComponent,
    GymRecordsComponent,
    AddGymRecordComponent,
    AddFitRecordComponent,
    FitRecordsComponent,
    ButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
