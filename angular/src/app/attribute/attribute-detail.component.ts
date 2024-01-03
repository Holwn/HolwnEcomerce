import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ManufacturerInListDto, ManufacturersService } from '@proxy/manufacturers';
import { ProductCategoriesService, ProductCategoryInListDto } from '@proxy/product-categories';
import { ProductDto, ProductsService } from '@proxy/products';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../shared/services/utility.service';
import { productTypeOptions } from '@proxy/holwn-ecommerce/products';
import { NotificationService } from '../shared/services/notification.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ProductAttributeDto, ProductAttributesService } from '@proxy/product-attributes';
import { attributeTypeOptions } from '@proxy/holwn-ecommerce/product-attributes';

@Component({
  selector: 'app-attribute-detail',
  templateUrl: './attribute-detail.component.html',
})
export class AttributeDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;
  public form: FormGroup;

  //Dropdown
  dataTypes: any[] = [];
  selectedEntity = {} as ProductAttributeDto;
  constructor(
    private attributeService: ProductAttributesService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utilService: UtilityService,
    private notificationService: NotificationService,
    private cd: ChangeDetectorRef,
    private sanitizer: DomSanitizer
  ) {}

  validationMessages = {
    code: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    label: [
      { type: 'required', message: 'Bạn phải nhập nhãn hiện thị' },
      { type: 'maxLength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    dataType: [{ type: 'required', message: 'Bạn phải chọn kiểu dữ liệu' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
  };

  ngOnDestroy(): void {
    if(this.ref){
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.buildForm();
    this.loadAttributeTypes();
    this.initFormData();
  }

  initFormData() {
    //Load edit data to form
    if(this.utilService.isEmpty(this.config.data?.id) == true){
      this.toggleBlockUI(false);
    }else{
      this.loadFormDetails(this.config.data?.id);
    }
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.attributeService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ProductAttributeDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  loadAttributeTypes() {
    attributeTypeOptions.forEach(element => {
      this.dataTypes.push({
        value: element.value,
        label: element.key,
      });
    });
  }

  saveChange() {
    this.toggleBlockUI(true);

    if(this.utilService.isEmpty(this.config.data?.id))
    {
      this.attributeService
      .create(this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next:()=>{
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        },
        error:(err)=>{
          this.notificationService.showError(err.error.error.message);
          this.toggleBlockUI(false);
        }
      })
    }else{
      this.attributeService
      .update(this.config.data?.id, this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next:()=>{
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        },
        error:(err)=>{
          this.notificationService.showError(err.error.error.message);
          this.toggleBlockUI(false);
        }
      })
    }
  }

  private buildForm() {
    this.form = this.fb.group({
      label: new FormControl(
        this.selectedEntity.label || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(250),
        ])
      ),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      dataType: new FormControl(this.selectedEntity.dataType || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || false),
      isRequired: new FormControl(this.selectedEntity.isRequired || false),
      isUnique: new FormControl(this.selectedEntity.isUnique || false),
      isActive: new FormControl(this.selectedEntity.isActive || false),
      note: new FormControl(this.selectedEntity.note || null)
    });
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
      this.btnDisabled = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
        this.btnDisabled = false;
      }, 1000);
    }
  }
}
