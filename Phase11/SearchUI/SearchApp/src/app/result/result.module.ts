import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResultComponent } from './result.component';
import { HeaderComponent } from './header/header.component';
import { ResultsComponent } from './results/results.component';
import { HomeModule } from '../home/home.module';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { ResultCardComponent } from './result-card/result-card.component';
import { MatCardModule } from '@angular/material/card';
import { HttpClientModule } from '@angular/common/http';
import { DocumentsService } from './service/documents.service';


@NgModule({
  declarations: [
    ResultComponent,
    HeaderComponent,
    ResultsComponent,
    ResultCardComponent
  ],
  imports: [
    CommonModule,
    HomeModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    MatCardModule,
    HttpClientModule
  ],
  exports: [
    ResultComponent
  ],
  providers: [
    DocumentsService
  ]
})
export class ResultModule { }
