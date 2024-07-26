import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { LeaveCategoryDto, LeaveCateoryAppServicesServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-or-editleave-cateory',
  templateUrl: './create-or-editleave-cateory.component.html',
  styleUrls: ['./create-or-editleave-cateory.component.css']
})
export class CreateOrEditleaveCateoryComponent extends AppComponentBase implements OnInit  {
  saving = false;
  @Input() id:number;
  @Output() onSave = new EventEmitter<any>();

  leaveCateory: LeaveCategoryDto = new LeaveCategoryDto();

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _cateoryService:LeaveCateoryAppServicesServiceProxy

   
  ) {
    super(injector);
  }
  ngOnInit(): void {
    
   }
 
  save(){
         this.saving = true;
         debugger
         if(this.leaveCateory.id){
         
          this._cateoryService.createOrEdit(this.leaveCateory).subscribe(
          () => {
          this.notify.info(this.l('UpdatedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
          },
          () => {
          this.saving = false;
          }
      );
        }
        else{
          debugger
            
              this._cateoryService.createOrEdit(this.leaveCateory).subscribe((response)=> {
              this.notify.info(this.l('SavedSuccessfully'));
              this.bsModalRef.hide();
              this.onSave.emit();
               },
              () => {
              this.saving = false;
   })
     }
       
  }
  
}
