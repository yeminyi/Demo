import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DemoModule } from "./demo/demo.module";

const routes: Routes = [
  { path: 'demo', loadChildren: ()=>DemoModule },
  { path: '**', redirectTo: 'demo' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
