import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardPageComponent } from './dashboard/dashboard-page/dashboard-page.component';
import { LoginPageComponent } from './authentication/login-page/login-page.component';
import { DownloadCardComponent } from './dashboard/download-card/download-card.component';

@NgModule({
  declarations: [AppComponent, DashboardPageComponent, LoginPageComponent, DownloadCardComponent],
  imports: [BrowserModule, AppRoutingModule, ReactiveFormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
