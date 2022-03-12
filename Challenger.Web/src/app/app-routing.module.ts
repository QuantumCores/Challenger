import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChallengeRulesComponent } from './components/challenge-rules/challenge-rules.component';
import { FitRecordsComponent } from './components/fit-records/fit-records.component';
import { ProductAddComponent } from './components/food/product-add/product-add.component';
import { DiaryRecordsComponent } from './components/food/diary-records/diary-records.component';
import { GymRecordsComponent } from './components/gym-records/gym-records.component';
import { HomeComponent } from './components/home/home.component';
import { MeasurementsComponent } from './components/measurements/measurements.component';
import { RankingComponent } from './components/ranking/ranking.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'ranking', component: RankingComponent },
  { path: 'measurements', component: MeasurementsComponent },
  { path: 'gymRecords', component: GymRecordsComponent },
  { path: 'fitRecords', component: FitRecordsComponent },
  { path: 'addProduct', component: ProductAddComponent },
  { path: 'diaryRecords', component: DiaryRecordsComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'rules', component: ChallengeRulesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
