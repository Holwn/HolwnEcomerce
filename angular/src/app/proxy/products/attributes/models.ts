import type { BaseListFilterDto } from '../../models';
import type { AttributeType } from '../../holwn-ecommerce/product-attributes/attribute-type.enum';

export interface AddUpdateProductAttributeDto {
  productId?: string;
  attributeId?: string;
  dateTimeValue?: string;
  decimalValue?: number;
  intValue?: number;
  varcharValue?: string;
  textValue?: string;
}

export interface ProductAttributeListFilterDto extends BaseListFilterDto {
  productId?: string;
}

export interface ProductAttributeValueDto {
  id?: string;
  productId?: string;
  attributeId?: string;
  code?: string;
  dataType: AttributeType;
  label?: string;
  dateTimeValue?: string;
  decimalValue?: number;
  intValue?: number;
  varcharValue?: string;
  textValue?: string;
  dateTimeId?: string;
  decimalId?: string;
  intId?: string;
  varcharId?: string;
  textId?: string;
}
