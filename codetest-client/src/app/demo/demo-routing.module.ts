import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DemoAppComponent } from './demo-app.component';
import { LogListComponent } from './components/log-list/log-list.component';
import { DataEntryComponent } from './components/data-entry/data-entry.component';

const routes: Routes = [
  {
    path: '', component: DemoAppComponent,
    children: [
      { path: 'log-list', component: LogListComponent },
      { path: 'data-entry', component: DataEntryComponent },
      { path: '**', redirectTo: 'data-entry'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DemoRoutingModule { }
