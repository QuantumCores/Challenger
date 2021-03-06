import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms'
import { JwtModule } from '@auth0/angular-jwt';
import { NgxEchartsModule } from 'ngx-echarts';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { MeasurementItemComponent } from './components/measurement-item/measurement-item.component';
import { AddMeasurementComponent } from './components/measurement-add/measurement-add.component';;
import { MeasurementsComponent } from './components/measurements/measurements.component';
import { GymRecordItemComponent } from './components/gym-record-item/gym-record-item.component';
import { FitRecordItemComponent } from './components/fit-record-item/fit-record-item.component';
import { GymRecordsComponent } from './components/gym-records/gym-records.component';
import { AddGymRecordComponent } from './components/gym-add-record/gym-add-record.component';
import { AddFitRecordComponent } from './components/fit-add-record/fit-add-record.component';
import { FitRecordsComponent } from './components/fit-records/fit-records.component';
import { ButtonComponent } from './components/button/button.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RankingComponent } from './components/ranking/ranking.component';
import { ChallengeRulesComponent } from './components/challenge-rules/challenge-rules.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

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
    ButtonComponent,
    HeaderComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    RankingComponent,
    ChallengeRulesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NoopAnimationsModule,
    JwtModule.forRoot(
      {
        config: {
          tokenGetter: tokenGetter,
          allowedDomains: ['localhost', 'localhost:7099', 'localhost:80', '54.37.137.86', '54.37.137.86:81'],
          disallowedRoutes: []
        }
      }),
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts')
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
