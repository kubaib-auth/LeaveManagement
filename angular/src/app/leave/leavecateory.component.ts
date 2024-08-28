import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateOrEditleaveCateoryComponent } from './create-or-editleave-cateory.component';
import { LeaveCategoryDto, LeaveCateoryAppServicesServiceProxy} from '@shared/service-proxies/service-proxies';

import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs';
class PagedRolesRequestDto extends PagedRequestDto {
  keyword: string;
  filterText: string;
  categoryNameFilter: string;
  skipCount: number;
  maxResultCount: number;

  constructor() {
    super();
    this.keyword = '';
    this.filterText = '';
    this.categoryNameFilter = '';
    this.skipCount = 0;
    this.maxResultCount = 10;
  }
}

@Component({
  selector: 'leavecateory',
  templateUrl: './leavecateory.component.html',
  styleUrls: ['./leavecateory.component.css']
})

export class LeavecateoryComponent extends AppComponentBase implements OnInit {
  leaveCateory: LeaveCategoryDto[] = [];
  PagedResultDtoOfLeaveCategoryDto = new LeaveCategoryDto();
  loading: boolean = false;
  @ViewChild(CreateOrEditleaveCateoryComponent) getLeave: CreateOrEditleaveCateoryComponent;
  keyword = '';
  pageNumber = 1;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _modalService: BsModalService,
    private _leaveCateoryServices: LeaveCateoryAppServicesServiceProxy,
  ) {
    super(injector);
    
  }

  ngOnInit() {
   // this.list(new PagedRolesRequestDto(), this.pageNumber, () => {});
  }

  createUser(id?: number): void {
    const createOrEditLeaveCateoryDialog: BsModalRef = this._modalService.show(
      CreateOrEditleaveCateoryComponent, {
      class: 'modal-lg',
      initialState: {
        id: id || null,
      },
    });

    createOrEditLeaveCateoryDialog.content.onSave.subscribe((result) => {
   // this.list(new PagedRolesRequestDto(), this.pageNumber, () => {});
    });
  }

  // list(
  //   request: PagedRolesRequestDto,
  //   pageNumber: number,
  //   finishedCallback: Function
  // ): void {
  //   request.keyword = this.keyword;
  //   request.skipCount = (pageNumber - 1) * request.maxResultCount;
  //   request.maxResultCount = 10;

  //   this._leaveCateoryServices
  //     .leaveCategory(
  //       request.keyword,
  //       request.filterText,
  //       request.categoryNameFilter,
  //       request.skipCount,
  //       request.keyword,
  //       request.maxResultCount,
  //       request.maxResultCount,
  //       undefined
  //     )
  //     .pipe(
  //       finalize(() => {
  //         finishedCallback();
  //       })
  //     ).subscribe((result: PagedResultDtoOfLeaveCategoryDto) => {
  //       this.leaveCateory = result.items;
  //       this.showPaging(result, pageNumber);
  //     });
  // }
}
