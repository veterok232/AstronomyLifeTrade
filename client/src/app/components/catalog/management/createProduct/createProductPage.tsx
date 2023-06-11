/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useMemo, useState } from "react";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { useParams } from "react-router-dom";
import { Row, Col, Button, Form } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { createProduct, createProductCharacteristics, editProduct, editProductCharacteristics, getBrands, getCategories, getProductForEdit } from "../../../../api/catalog/catalogApi";
import { uploadFile } from "../../../../api/file/filesApi";
import { Local } from "../../../localization/local";
import arrayMutators from "final-form-arrays";
import { BrandModel } from "../../../../dataModels/catalog/brandModel";
import { CategoryModel } from "../../../../dataModels/catalog/categoryModel";
import { generateRandomString } from "../../../../utils/stringUtils";
import { TextFormControl } from "../../../common/controls/formControls/textFormControl";
import { requiredNonWhitespace, required, nonWhitespace } from "../../../common/controls/validation/validators";
import { RichSelectFormControl } from "../../../common/controls/formControls/richSelectFormControl";
import { dataToOptions } from "../../../../utils/dataTransformUtils";
import { TextAreaFormControl } from "../../../common/controls/formControls/textareaFormControl";
import { ProductCharacteristics } from "../../../../dataModels/catalog/productDetails/productCharacteristics";
import { TelescopeCharacteristicsSection } from "../characteristics/telescopeCharacteristicsSection";
import { FileFormControl } from "../../../common/controls/formControls/fileFormControl";
import { FileExtensions } from "../../../fileExtensions";
import { CurrencyFormControl } from "../../../common/controls/formControls/currencyFormControl";
import { CategoryType } from "../../../../dataModels/enums/categoryType";
import { NoData } from "../../../common/presentation/noData";
import { notifications } from "../../../toast/toast";
import { sharedHistory } from "../../../../infrastructure/sharedHistory";
import { getRoute } from "../../../../utils/routeUtils";
import { routeLinks } from "../../../layout/routes/routeLinks";
import { TelescopeCharacteristics } from "../../../../dataModels/catalog/productDetails/telescopeCharacteristics";
import { IntegerUpDownFormControl } from "../../../common/controls/formControls/maskedFormControls/integerUpDownFormControl";
import { localizer } from "../../../localization/localizer";
import { Dictionary } from "lodash";
import { EnumDictionary } from "../../../../infrastructure/enums/enumDictionary";
import { LabeledValue } from "../../../common/controls/labeledValue";
import { BinocularsCharacteristicsSection } from "../characteristics/binocularsCharacteristicsSection";
import { BinocularCharacteristics } from "../../../../dataModels/catalog/productDetails/binocularCharacteristics";
import { AccessoriesCharacteristicsSection } from "../characteristics/accessoriesCharacteristicsSection";
import { AccessoryCharacteristics } from "../../../../dataModels/catalog/productDetails/accessoryCharacteristics";

export interface CreateProductCharacteristicsModel {
    productId: string;
    categoryId: string;
    telescopeCharacteristics: TelescopeCharacteristics;
    binocularCharacteristics: BinocularCharacteristics;
    accessoryCharacteristics: AccessoryCharacteristics;
}

export interface CreateProductModel {
    name: string;
    code: string;
    description: string;
    brandId: string;
    categoryId: string;
    price: number;
    manufacturer: string;
    quantity: number;
    equipment: string;
    shortDescription: string;
    productImages: File[];
    productFiles: File[];
}

export interface ProductForEditModel {
    name: string;
    code: string;
    description: string;
    brandId: string;
    categoryId: string;
    price: number;
    manufacturer: string;
    quantity: number;
    equipment: string;
    shortDescription: string;
    characteristics: CreateProductCharacteristicsModel;
    productId: string;
}

export interface EditProductModel {
    name: string;
    code: string;
    description: string;
    brandId: string;
    categoryId: string;
    price: number;
    manufacturer: string;
    quantity: number;
    equipment: string;
    shortDescription: string;
    productId: string;
}

export interface CreateProductFormData {
    name: string;
    code: string;
    description: string;
    brandId: string;
    categoryId: string;
    price: number;
    manufacturer: string;
    quantity: number;
    equipment: string;
    shortDescription: string;
    characteristics: ProductCharacteristics;
    productImages: File[];
    productFiles: File[];
}

const categoryTypesToNames: EnumDictionary<CategoryType, string> = {
    [CategoryType.Telescopes]: "Telescopes",
    [CategoryType.Binoculars]: "Binoculars",
    [CategoryType.Accessories]: "Accessories",
};

export const CreateProductPage = () => {
    const { productId } = useParams<{ productId: string }>();
    const [fileInputKey, setFileInputKey] = useState(generateRandomString());
    const [brands, setBrands] = useState<BrandModel[]>();
    const [categories, setCategories] = useState<CategoryModel[]>();
    const [productModel, setProductModel] = useState<ProductForEditModel>();
    const [selectedCategoryId, setSelectedCategoryId] = useState<string>();

    const brandsAsOptions = useMemo(
        () =>
            dataToOptions(
                brands, /* ?.filter(b => categoryTypesToNames[b.categoryType] === categories?.find(c => c.id === selectedCategoryId)?.name) */
                (b: BrandModel) => b.id,
                (b: BrandModel) => `${b.name} - ${localizer.get(categoryTypesToNames[b.categoryType])}`,
            ),
        [brands]
    );

    const categoriesAsOptions = useMemo(
        () =>
            dataToOptions(
                categories,
                (c: CategoryModel) => c.id,
                (c: CategoryModel) => localizer.get(c.name),
            ),
        [categories]
    );

    useAsyncEffect(async () => {
        if (productId) {
            setProductModel(await getProductForEdit(productId));
        }

        setBrands(await getBrands());
        setCategories(await getCategories());
    }, []);

    const getCharacteristicsSection = (categoryId: string) => {
        if (categories?.find(c => c.id == categoryId)?.name === "Telescopes") {
            return <TelescopeCharacteristicsSection />;
        } else if (categories?.find(c => c.id == categoryId)?.name === "Binoculars") {
            return <BinocularsCharacteristicsSection />;
        } else if (categories?.find(c => c.id == categoryId)?.name === "Accessories") {
            return <AccessoriesCharacteristicsSection />;
        }

        return <NoData localizationKey="CreateProduct_SelectProductCategory" />;
    };

    const getProductCharacteristics = (productModel: ProductForEditModel): ProductCharacteristics => {
        if (categories?.find(c => c.id == productModel?.categoryId)?.name === "Telescopes") {
            return productModel.characteristics.telescopeCharacteristics;
        } else if (categories?.find(c => c.id == productModel?.categoryId)?.name === "Binoculars") {
            return productModel.characteristics.binocularCharacteristics;
        } else if (categories?.find(c => c.id == productModel?.categoryId)?.name === "Accessories") {
            return productModel.characteristics.accessoryCharacteristics;
        }
    };

    const onSubmit = async (formData: CreateProductFormData) => {
        if (!productId) {
            const result = await createProduct({ ...formData });

            if (!result.isSucceeded) {
                notifications.localizedError(result.errors[0]);
                return;
            }

            await createProductCharacteristics({
                telescopeCharacteristics: { ...formData.characteristics },
                binocularCharacteristics: { ...formData.characteristics },
                accessoryCharacteristics: { ...formData.characteristics },
                productId: result.data,
                categoryId: formData.categoryId,
            });

            sharedHistory.push(getRoute(routeLinks.catalog.root));
            return;
        }

        const result = await editProduct({
            ...formData,
            productId: productId,
        });

        if (!result.isSucceeded) {
            notifications.localizedError(result.errors[0]);
            return;
        }

        await editProductCharacteristics({
            telescopeCharacteristics: { ...formData.characteristics },
            binocularCharacteristics: { ...formData.characteristics },
            accessoryCharacteristics: { ...formData.characteristics },
            productId: result.data,
            categoryId: formData.categoryId,
        });

        sharedHistory.push(getRoute(routeLinks.catalog.root));
    };

    return (
        <FinalForm
        onSubmit={onSubmit}
        mutators={{...arrayMutators}}
        initialValues={{
            ...productModel,
            quantity: 0,
            characteristics: {...getProductCharacteristics(productModel)}}}
        render={({ values, handleSubmit }: FormRenderProps<CreateProductFormData>) => (
            <Form onSubmit={handleSubmit}>
                <div className="product-details">
                    <Row className="mb-3">
                        <Col>
                            <h1 className="ui-page-header pt-2">
                                <Local id={`${productId ? "EditProduct_Title" : "CreateProduct_Title"}`} />
                            </h1>
                        </Col>
                    </Row>
                    <Row className="product-details top-box row-cols-2 p-4 mb-3">
                        <Col className="pl-0">
                            <FileFormControl
                                name={"productImages"}
                                label="UploadImages"
                                accept={[FileExtensions.jpg]}
                                multiple />
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="GeneralDetails" /></h1>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <TextFormControl label={"ProductName"} name={"name"} placeholder="Наименование товара"
                                        validator={requiredNonWhitespace} markRequired />
                                </Col>
                                <Col className="pl-0">
                                    <TextFormControl label={"Article"} name={"code"} placeholder="Артикул"
                                        validator={requiredNonWhitespace} markRequired />
                                </Col>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <TextFormControl label={"ShortDescription"} name={"shortDescription"} placeholder="Краткое описание товара"
                                        validator={nonWhitespace} />
                                </Col>
                                <Col className="pl-0">
                                    <TextFormControl label={"SpecialNote"} name={"specialNote"} placeholder="Особое замечание"
                                        validator={nonWhitespace} />
                                </Col>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <RichSelectFormControl
                                        label={"Category"}
                                        name={"categoryId"}
                                        clearable
                                        options={categoriesAsOptions}
                                        validator={required} />
                                </Col>
                                <Col className="pl-0">
                                    <RichSelectFormControl
                                        label={"Brand"}
                                        name={"brandId"}
                                        clearable
                                        options={brandsAsOptions}
                                        validator={required} />
                                </Col>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <CurrencyFormControl textPosition="left" integerDigitsLimit={7} label={"Price"} name={"price"} validator={required} />
                                </Col>
                                <Col className="pl-0">
                                    <TextFormControl label={"Manufacturer"} name={"manufacturer"} placeholder="Производитель"
                                        validator={nonWhitespace} />
                                </Col>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    {productId &&
                                        <span className="field-label mb-2">Изменить количество товара (сейчас на складе: {productModel?.quantity} шт.)</span>}
                                    <IntegerUpDownFormControl
                                        minValue={-productModel?.quantity}
                                        maxValue={100}
                                        name={"quantity"}
                                        className="w-100 mt-2" />
                                </Col>
                                <Col className="pl-0">
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="Description" /></h1>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <TextAreaFormControl name={"description"} placeholder="Описание товара"
                                        validator={requiredNonWhitespace} markRequired maxLength={5000} />
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="Equipment" /></h1>
                            </Row>
                            <Row className="section-description">
                                <p>Позиции в комплектации товара разделяйте символом {"\";\""}</p>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <TextAreaFormControl name={"equipment"} placeholder="Комплектация товара"
                                        validator={requiredNonWhitespace} markRequired maxLength={5000} />
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="Characteristics" /></h1>
                            </Row>
                            {getCharacteristicsSection(values.categoryId)}
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="Instructions" /></h1>
                            </Row>
                            <Row>
                                <Col className="pl-0">
                                    <FileFormControl
                                        name={"productFiles"}
                                        label="UploadInstructions"
                                        accept={[FileExtensions.pdf]}
                                        multiple />
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    <hr />
                    <Row className="justify-content-end">
                        <Col className="p-0">
                            <Button className="cart-button ml-3 float-right ml-3" type="submit">
                                <Local id="Save" />
                            </Button>
                            <Button className="cart-button mr-2 float-right" onClick={() => sharedHistory.push(getRoute(routeLinks.catalog.root))}>
                                <Local id="Cancel" />
                            </Button>
                        </Col>
                    </Row>
                </div>
            </Form>)}
        />
    );
};