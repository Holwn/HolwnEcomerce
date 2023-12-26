import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductCategoriesService, ProductCategoryInListDto } from '@proxy/product-categories';
import { ProductDto, ProductInListDto, ProductsService } from '@proxy/products';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ProductDetailComponent } from './product-detail.component';
import { NotificationService } from '../shared/services/notification.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ProductInListDto[] = [];
  public selectedItems: ProductInListDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //Filter
  productcategories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
    private productService: ProductsService,
    private productCategoryService: ProductCategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadProductCategories();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.productService
      .getListFilter({
        keyword: this.keyword,
        categoryId: this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ProductInListDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
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
          label: element.name,
        });
      });
    });
  }

  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  showAddModal() {
    const ref = this.dialogService.open(ProductDetailComponent,{
      header:'Thêm mới sản phẩm',
      width:'70%',
    })

    ref.onClose.subscribe((data: ProductDto)=>{
      if(data){
        this.loadData();
        this.notificationService.showSuccess("Thêm sản phẩm thành công");
        this.selectedItems = [];
      }
    })
  }

  showEditModal() {
    if(this.selectedItems.length == 0){
      this.notificationService.showError('Bạn phải chọn một bản ghi');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(ProductDetailComponent,{
      data:{
        id: id
      },
      header:'Cập nhật sản phẩm',
      width:'70%',
    })

    ref.onClose.subscribe((data: ProductDto)=>{
      if(data){
        this.loadData();
        this.notificationService.showSuccess("Cập nhật sản phẩm thành công");
        this.selectedItems = [];
      }
    })
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }
}
