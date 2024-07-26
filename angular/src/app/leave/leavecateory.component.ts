import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateOrEditleaveCateoryComponent } from './create-or-editleave-cateory.component';
import { LeaveCateoryAppServicesServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'leavecateory',
  templateUrl: './leavecateory.component.html',
  styleUrls: ['./leavecateory.component.css']
})
export class LeavecateoryComponent extends AppComponentBase implements OnInit {
  leaveCateory:any[]=[];
  loading: boolean = false; 
  @ViewChild(CreateOrEditleaveCateoryComponent) getLeave :CreateOrEditleaveCateoryComponent;

  constructor(
    injector: Injector,      
    public bsModalRef: BsModalRef,
    private _modalService: BsModalService
    
  ) {
    super(injector);
  }

  ngOnInit() { 
    
  } 
  createUser(id?:number):void{
    
    const createOrEditLeaveCateoryDialog:BsModalRef = this._modalService.show(
      CreateOrEditleaveCateoryComponent,{
       class:'modal-lg',
       initialState:{
         id:id || null,              
       },
     }
  );
  createOrEditLeaveCateoryDialog.content.onSave.subscribe((result)=>{
      
     // this.getLeaveAll();
   });
}
}
