import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ApproveRejectedLeave, EnumLeave, GetAllLeaveDto, LeaveAppServicesServiceProxy, LeaveDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-approve-or-rejectedleav',
  templateUrl: './approve-or-rejectedleav.component.html',
  styleUrls: ['./approve-or-rejectedleav.component.css']
})
export class ApproveOrRejectedleavComponent extends AppComponentBase implements OnInit {


@Input() id:number;
asas: GetAllLeaveDto = new GetAllLeaveDto();
leave:any;
@Output() onSave = new EventEmitter<any>();

leavetypeday:EnumLeave = undefined;
leaveStatus:ApproveRejectedLeave = undefined;
saving = false;
approveId: number;
rejectId: number;
constructor(injector: Injector,     public bsModalRef: BsModalRef,
private _leaveService:LeaveAppServicesServiceProxy,
){
  super(injector);
}
  ngOnInit(): void {
    this.receiveId()
  }
  receiveId(){
    this._leaveService.getDetailId(this.id).subscribe((response)=>{
      debugger
      this.leave = response;
          this.leavetypeday= this.leave.status;
          this.leaveStatus= this.leave.isLeave 
  })
  }
  approve(approveId:number) {
    debugger
    this.saving = true;
      this._leaveService.approvedLeave(approveId).subscribe(
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
    rejected(rejectId) {
      debugger
      this.saving = true;
        this._leaveService.rejectedLeave(rejectId).subscribe(
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
    
}
