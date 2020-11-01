import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LogListComponent } from './components/log-list/log-list.component';
import { DataEntryComponent } from './components/data-entry/data-entry.component';
import { DemoRoutingModule } from './demo-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { DemoAppComponent } from './demo-app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { ScoreService } from './services/score.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EnsureAcceptHeaderInterceptor } from '../shared/ensure-accept-header.interceptor';
import { HandleHttpErrorInterceptor } from '../shared/handle-http-error-interceptor';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
@NgModule({
  imports: [
    CommonModule,
    DemoRoutingModule,
    HttpClientModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    InfiniteScrollModule
  ],
  declarations: [
    DemoAppComponent,
    SidenavComponent,
    ToolbarComponent,
    LogListComponent,
    DataEntryComponent,
    ConfirmDialogComponent,
    DemoAppComponent
  ],
  providers: [
    ScoreService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: EnsureAcceptHeaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HandleHttpErrorInterceptor,
      multi: true,
    }
  ]
})
export class DemoModule { }
