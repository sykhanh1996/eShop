import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AhahahComponent } from './ahahah/ahahah.component';


const routes: Routes = [
    {
        path: '',
        component: AhahahComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class testRoutingModule {}
