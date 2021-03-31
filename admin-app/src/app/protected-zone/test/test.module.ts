import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AhahahComponent } from './ahahah/ahahah.component';
import { testRoutingModule } from './test-routing.module';



@NgModule({
  declarations: [AhahahComponent],
  imports: [
    CommonModule,
    testRoutingModule
  ]
})
export class TestModule { }
