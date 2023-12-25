import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ManufacturerInListDto, ManufacturersService } from '@proxy/manufacturers';
import { ProductCategoriesService, ProductCategoryInListDto } from '@proxy/product-categories';
import { ProductDto, ProductInListDto, ProductsService } from '@proxy/products';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../shared/services/utility.service';
import { productTypeOptions } from '@proxy/holwn-ecommerce/products';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  btnDisabled = false;
  blockedPanel: boolean = false;

  public form: FormGroup;

  //Dropdown
  productcategories: any[] = [];
  manufactureres: any[] = [];
  productTypes: any[] = [];
  selectedEntity = {} as ProductDto;
  constructor(
    private productService: ProductsService,
    private productCategoryService: ProductCategoriesService,
    private manufacturerService: ManufacturersService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utilService: UtilityService
  ) {}

  validationMessages = {
    code: [{ type:'required', message:'Bạn phải nhập mã duy nhất' }],
    name: [
      { type:'required', message:'Bạn phải nhập tên sản phẩm' },
      { type:'minLength', message:'Bạn phải nhập ít nhất 1 kí tự' },
      { type:'maxLength', message:'Bạn không được nhập quá 255 kí tự' }
    ],
    slug: [{ type:'required', message:'Bạn phải nhập URL duy nhất' }],
    sku: [{ type:'required', message:'Bạn phải nhập mã SKU sản phẩm' }],
    manufacturerId: [{ type:'required', message:'Bạn phải chọn nhà cung cấp' }],
    categoryId: [{ type:'required', message:'Bạn phải chọn danh mục sản phẩm' }],
    productType: [{ type:'required', message:'Bạn phải chọn loại sản phẩm' }],
    sortOrder: [{ type:'required', message:'Bạn phải nhập thứ tự' }],
    sellPrice: [{ type:'required', message:'Bạn phải nhập giá bán cho sản phẩm' }],
  }

  ngOnDestroy(): void {}

  ngOnInit(): void {
    this.buildForm();
    this.loadProductTypes();

    //Load data to form
    var productcategories = this.productCategoryService.getListAll();
    var manufactureres = this.manufacturerService.getListAll();
    this.toggleBlockUI(true);
    forkJoin({
      productcategories,
      manufactureres
    }).pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next:(response:any)=>{
        //Push data to dropdown
        var productCategories = response.productcategories as ProductCategoryInListDto[];
        var manufactureres = response.manufactureres as ManufacturerInListDto[];
        productCategories.forEach(element =>{
          this.productcategories.push({
            value: element.id,
            label: element.name
          })
        });

        manufactureres.forEach(element =>{
          this.manufactureres.push({
            value: element.id,
            label: element.name
          })
        });

        //Load edit data to form
        if(this.utilService.isEmpty(this.config.data?.id)){
          this.toggleBlockUI(false);
        }else{
          this.loadFormDetails(this.config.data?.id);
        }
      },
      error:()=>{
        this.toggleBlockUI(false);
      }
    })
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.productService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ProductDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  loadProductTypes() {
    productTypeOptions.forEach(element => {
      this.productTypes.push({
        value: element.value,
        label: element.key
      });
    });
  }

  generateSlug(){
    this.form.controls['slug'].setValue(this.utilService.MakeSeoTitle(this.form.get('name').value));
  }

  saveChange(){
    
  }

  private buildForm() {
    this.form = this.fb.group({
      name: new FormControl(this.selectedEntity.name || null, Validators.compose([
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250)
      ])),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      sku: new FormControl(this.selectedEntity.sku || null, Validators.required),
      manufacturerId: new FormControl(this.selectedEntity.manufacturerId || null, Validators.required),
      categoryId: new FormControl(this.selectedEntity.categoryId || null, Validators.required),
      productType: new FormControl(this.selectedEntity.productType || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      sellPrice: new FormControl(this.selectedEntity.sellPrice || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || true),
      isActive: new FormControl(this.selectedEntity.isActive || true),
      seoMetaDescription: new FormControl(this.selectedEntity.seoMetaDescription || null),
      description: new FormControl(this.selectedEntity.description || null),
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
