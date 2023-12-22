import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductCategoriesService, ProductCategoryInListDto } from '@proxy/product-categories';
import { ProductDto, ProductInListDto, ProductsService } from '@proxy/products';
import { Subject, takeUntil } from 'rxjs';

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
    private fb: FormBuilder
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

  loadProductCategories() {
    this.productCategoryService.getListAll().subscribe((response: ProductCategoryInListDto[]) => {
      response.forEach(element => {
        this.productcategories.push({
          value: element.id,
          name: element.name,
        });
      });
    });
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
      this.btnDisabled = false;
      this.blockedPanel = false;
      }, 1000);
    }
  }
}
