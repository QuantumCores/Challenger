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
import { MatTimepickerModule } from 'mat-timepicker';

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
import { ProductAddComponent } from './components/food/product-add/product-add.component';
import { DishAddComponent } from './components/food/dish-add/dish-add.component';
import { IngridientAddComponent } from './components/food/ingridient-add/ingridient-add.component';
import { DiaryAddRecordComponent } from './components/food/diary-add-record/diary-add-record.component';
import { MealAddRecordComponent } from './components/food/meal-add-record/meal-add-record.component';
import { DiaryRecordsComponent } from './components/food/diary-records/diary-records.component';
import { MealRecordItemComponent } from './components/food/meal-record-item/meal-record-item.component';
import { ProductItemComponent } from './components/food/product-item/product-item.component';
import { MealProductAddComponent } from './components/food/meal-product-add/meal-product-add.component';
import { ProductSearchComponent } from './components/food/product-search/product-search.component';
import { MealProductItemComponent } from './components/food/meal-product-item/meal-product-item.component';
import { MealDishAddComponent } from './components/food/meal-dish-add/meal-dish-add.component';
import { FastRecordAddComponent } from './components/food/fast-record-add/fast-record-add.component';
import { FastRecordItemComponent } from './components/food/fast-record-item/fast-record-item.component';
import { DishSearchComponent } from './components/food/dish-search/dish-search.component';
import { IngridientItemComponent } from './components/food/ingridient-item/ingridient-item.component';
import { MealDishItemComponent } from './components/food/meal-dish-item/meal-dish-item.component';

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
    ChallengeRulesComponent,
    ProductAddComponent,
    DishAddComponent,
    IngridientAddComponent,
    DiaryAddRecordComponent,
    MealAddRecordComponent,
    DiaryRecordsComponent,
    MealRecordItemComponent,
    ProductItemComponent,
    MealProductAddComponent,
    ProductSearchComponent,
    MealProductItemComponent,
    MealDishAddComponent,
    FastRecordAddComponent,
    FastRecordItemComponent,
    DishSearchComponent,
    IngridientItemComponent,
    MealDishItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NoopAnimationsModule,
    MatTimepickerModule,
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
