import React, { useState } from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { Row, Col, Button, Form } from "reactstrap";
import { RichSelectFormControl } from "../common/controls/formControls/richSelectFormControl";
import { TextFormControl } from "../common/controls/formControls/textFormControl";
import { requiredNonWhitespace } from "../common/controls/validation/validators";
import { Local } from "../localization/local";
import { LabeledValue } from "../common/controls/labeledValue";
import { isNumber } from "lodash";
import { CalculatorParameter } from "../common/presentation/calculatorParameter";
import { showNotificationIfInvalid } from "../common/controls/validation/formValidators";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { getMostMatchingTelescopes } from "../../api/astronomicalCalculator/astronomicalCalculatorApi";
import { ProductCard } from "../catalog/product/productCard";
import { onAddToCart, onAddToFavorites, onDeleteProduct } from "../catalog/catalogActions";
import { getRoute } from "../../utils/routeUtils";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { routeLinks } from "../layout/routes/routeLinks";
import { NoData } from "../common/presentation/noData";

interface AstronomicalCalculatorResult {
    scale: number;
    fieldOfView: number;
    outputPupil: number;
    maxScale: number;
    resolvingScale: number;
    bigScale: number;
    penetratingScale: number;
    mediumScale: number;
    moderateScale: number;
    equalPupilScale: number;

    maxScaleOcular: number;
    resolvingScaleOcular: number;
    bigScaleOcular: number;
    penetratingScaleOcular: number;
    mediumScaleOcular: number;
    moderateScaleOcular: number;
    equalPupilScaleOcular: number;

    relativeAperture: number;
    resolutionCapability: number;
    maxStellarMagnutude: number;
    moonCraterSize: number;
}

interface AstronomicalCalculatorFormData {
    aperture: number;
    focusDistance: number;
    ocularFocusDistance: number;
    ocularFov: number;
    balowLens?: number;
    calculationResult: AstronomicalCalculatorResult;
}

const barlowLensOptions: LabeledValue[] = [{
    label: "1.5x",
    value: 1.5,
}, {
    label: "2x",
    value: 2,
}];

function deg2hms(deg: number) {
    let deg1 = deg;
    /* Add 0.5", so floor will work as round */
    deg1 += 0.5 / (3600);
    const d = Math.floor(deg1);
    const m = Math.floor((deg1 - d) * 60);
    const s = Math.floor(60 * ((deg1 - d) * 60 - m));
    return d + "° " + m + "' " + s + "\"";
}


function formatPow(p: number)
{
    if (isNumber(p)) {
        return p.toFixed(2) + "x";
    }
}


function formatEP(ep: number)
{
    if (isNumber(ep)) {
        return ep.toFixed(2) + "мм";
    }
}


function formatSec(s: number)
{
    if (isNumber(s)) {
        return s.toFixed(2) + "\"";
    }
}


function log10(x: number)
{
  return Math.LOG10E * Math.log(x);
}

export const AstronomicalCalculatorPage = () => {
    const [calculationResult, setCalculationResult] = useState<AstronomicalCalculatorResult>();
    const [products, setProducts] = useState<ProductListItem[]>();

    const onSubmit = async (data: AstronomicalCalculatorFormData) => {
        const Pmax = 2.0 * data.aperture;
        const Praz = 1.4 * data.aperture;
        const Pbig = 1.0 * data.aperture;
        const Ppro = 0.7 * data.aperture;
        const Pmid = data.aperture / 2.0;
        const Pumr = data.aperture / 3.0;
        const Pmin = data.aperture / 6.0;

        let Ff = 0.0;
        let Raz = 0.0;
        /* let Dawes = 0.0; */
        let Moon = 0.0;

        if (data.aperture > 0)
        {
            Raz = 140.0 / data.aperture;
            /* Dawes = 115.8 / data.aperture; */
            Ff = data.focusDistance / data.aperture;
            Moon = 2.0 * Raz * 3476.0 / 1865.0;
        }

        const M = 7.5 + 5.0 * log10(data.aperture / 10.0);
        let Pok = 0;

        if (data.ocularFocusDistance > 0) {
            Pok = data.focusDistance / data.ocularFocusDistance;
        }

        let Aok = 0;
        let epv = 0;

        if (Pok > 0) {
            Aok = data.ocularFov / Pok;
            epv = data.aperture / Pok;
        }

        if (data.balowLens)
        {
            if (data.balowLens > 0) {
                Pok *= data.balowLens;
                Aok /= data.balowLens;
                epv = data.aperture / Pok;
            }
        }

        const calculationResult: AstronomicalCalculatorResult = {
            scale: Pok,
            fieldOfView: Aok,
            outputPupil: epv,

            maxScale: Pmax,
            resolvingScale: Praz,
            bigScale: Pbig,
            penetratingScale: Ppro,
            mediumScale: Pmid,
            moderateScale: Pumr,
            equalPupilScale: Pmin,

            maxScaleOcular: data.focusDistance / Pmax,
            resolvingScaleOcular: data.focusDistance / Praz,
            bigScaleOcular: data.focusDistance / Pbig,
            penetratingScaleOcular: data.focusDistance / Ppro,
            mediumScaleOcular: data.focusDistance / Pmid,
            moderateScaleOcular: data.focusDistance / Pumr,
            equalPupilScaleOcular: data.focusDistance / Pmin,

            relativeAperture: Ff,
            resolutionCapability: Raz,
            maxStellarMagnutude: M,
            moonCraterSize: Moon,
        };

        setCalculationResult(calculationResult);

        const matchingTelescopes = await getMostMatchingTelescopes({
            aperture: data.aperture,
            focusDistance: data.focusDistance,
            maxScale: Pmax,
        });

        setProducts(matchingTelescopes);
    };

    return (
        <FinalForm
        onSubmit={onSubmit}
        render={({ handleSubmit, valid }: FormRenderProps<AstronomicalCalculatorFormData>) => (
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Col>
                        <h1 className="ui-page-header pt-2"><Local id="AstronomicalCalculator_Title" /></h1>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="AstronomicalCalculator_Description" /></h1>
                        </Row>
                        <Row>
                            <Col className="pl-0 section-description">
                                <p>
                                    С помощью этого инструмента Вы сможете узнать о возможностях Вашего телескопа и определиться с выбором окуляров для него. Он рассчитает параметры и характеристики телескопа (увеличение, поле зрения, выходной зрачок) при использовании разных окуляров.
                                </p>
                            </Col>
                        </Row>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="Telescope" /></h1>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <TextFormControl label={"Aperture"} name={"aperture"} placeholder="Диаметр телескопа"
                                    validator={requiredNonWhitespace} markRequired />
                            </Col>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <TextFormControl label={"FocusDistance"} name={"focusDistance"} placeholder="Фокусное расстояние"
                                    validator={requiredNonWhitespace} markRequired />
                            </Col>
                        </Row>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="Ocular" /></h1>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <TextFormControl label={"OcularFocusDistance"} name={"ocularFocusDistance"} placeholder="Фокусное расстояние окуляра"
                                    validator={requiredNonWhitespace} markRequired />
                            </Col>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <TextFormControl label={"OcularFov"} name={"ocularFov"} placeholder="Поле зрения окуляра"
                                    validator={requiredNonWhitespace} markRequired />
                            </Col>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <RichSelectFormControl
                                    label={"BarlowLens"}
                                    name={"barlowLens"}
                                    clearable
                                    options={barlowLensOptions} />
                            </Col>
                        </Row>
                        <Row className="mt-3">
                            <Col>
                                <Button type="submit" onClick={() => showNotificationIfInvalid(valid)}>
                                    <Local id="Calculate" />
                                </Button>
                            </Col>
                        </Row>
                    </Col>
                </Row>
                {calculationResult && <>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="TelescopeParametersWithOcular" /></h1>
                            </Row>
                            <Row>
                                <Col>
                                    <CalculatorParameter name={"Scale"} value={formatPow(calculationResult.scale)} />
                                    <CalculatorParameter name={"FieldOfView"} value={deg2hms(calculationResult.fieldOfView) + " (" + calculationResult.fieldOfView.toFixed(3) + "°)"} />
                                    <CalculatorParameter name={"OutputPupil"} value={formatEP(calculationResult.outputPupil)} />
                                    <CalculatorParameter name={"RelativeAperture"} value={"1/" + calculationResult.relativeAperture.toFixed(2)} />
                                    <CalculatorParameter name={"ResolutionCapability"} value={formatSec(calculationResult.resolutionCapability)} />
                                    <CalculatorParameter name={"MaxStellarMagnutude"} value={calculationResult.maxStellarMagnutude.toFixed(1)} />
                                    <CalculatorParameter name={"MoonCraterSize"} value={calculationResult.moonCraterSize.toFixed(1) + "км"} />
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="TypicalScales" /></h1>
                            </Row>
                            <Row>
                                <Col>
                                    <CalculatorParameter name={"MaxScale"} value={formatPow(calculationResult.maxScale) + `(с окуляром ${formatEP(calculationResult.maxScaleOcular)})`} />
                                    <CalculatorParameter name={"ResolvingScale"} value={formatPow(calculationResult.resolvingScale) + `(с окуляром ${formatEP(calculationResult.resolvingScaleOcular)})`} />
                                    <CalculatorParameter name={"BigScale"} value={formatPow(calculationResult.bigScale) + `(с окуляром ${formatEP(calculationResult.bigScaleOcular)})`} />
                                    <CalculatorParameter name={"PenetratingScale"} value={formatPow(calculationResult.penetratingScale) + `(с окуляром ${formatEP(calculationResult.penetratingScaleOcular)})`} />
                                    <CalculatorParameter name={"MediumScale"} value={formatPow(calculationResult.mediumScale) + `(с окуляром ${formatEP(calculationResult.mediumScaleOcular)})`} />
                                    <CalculatorParameter name={"ModerateScale"} value={formatPow(calculationResult.moderateScale) + `(с окуляром ${formatEP(calculationResult.moderateScaleOcular)})`} />
                                    <CalculatorParameter name={"EqualPupilScale"} value={formatPow(calculationResult.equalPupilScale) + `(с окуляром ${formatEP(calculationResult.equalPupilScaleOcular)})`} />
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    <Row className="order-step-card p-3 mb-2">
                        <Col>
                            <Row className="mb-2">
                                <h1 className="ui-section-header pt-2"><Local id="MostMatchingTelescopes" /></h1>
                            </Row>
                            <Row className="">
                                {products?.length > 0
                                    ? products.map((product, ind) =>
                                        <ProductCard
                                            className="col-2"
                                            key={ind}
                                            product={product}
                                            onAddToFavorites={async () => await onAddToFavorites(product.productId)}
                                            onAddToCart={async () => await onAddToCart(product.productId)}
                                            onEditProduct={() => sharedHistory.push(getRoute(routeLinks.catalog.editProduct, product.productId))}
                                            onDeleteProduct={() => onDeleteProduct(product.productId)} />)
                                    : <NoData localizationKey="Calculator_NoTelescopesFound"/>}
                            </Row>
                        </Col>
                    </Row>
                </>}
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="ParametersDescription" /></h1>
                        </Row>
                        <Row>
                            <Col className="pl-0 d-flex justify-content-center mb-3">
                                <img className="astro-calc-image" src="static/images/astro_calc1.jpg"/>
                            </Col>
                        </Row>
                        <Row>
                            <Col className="section-description pl-0">
                                <p>
                                <b>Увеличение телескопа</b> рассчитывается как его фокусное расстояние разделенное на фокусное расстояние окуляра. Также на увеличение влияет линза Барлоу: увеличение возрастает в соответствии с кратностью линзы.
                                </p>
                                <p>
                                <b>Поле зрения телескопа</b> - это угловые размеры участка неба, видимого в телескоп с данным окуляром. Поле зрения телескопа зависит от увеличения и поля зрения окуляра. Для сравнения - средний видимый диаметр Луны равен 0° 31′ 5″ (0.518°).
                                </p>
                                <p>
                                <b>Выходной зрачок</b> - это диаметр изображения, которое формируется окуляром. Чем больше выходной зрачок, тем ярче изображение. Выходной зрачок рассчитывается путем деления диаметра объектива на увеличение.
                                </p>
                                <p>
                                <b>Максимальное увеличение телескопа</b> рассчитывается как удвоенный диаметр объектива. Обычно нет смысла ставить увеличения выше этого значения. При увеличениях, больше максимального, будет сложно сфокусировать изображение, усилятся вибрации изображения, при этом никакого выигрыша по количеству деталей не будет. Обычно применяется при наблюдении тесных двойных звезд и юстировке телескопа. При отличных условиях наблюдения может применяться для наблюдения деталей на диске планет, Луны и Солнца.
                                </p>
                                <p>
                                <b>При разрешающем увеличении</b> как правило достигается предел по разрешающей способности телескопа. Повышение увеличения выше разрешающего обычно не дает значительного эффекта по разрешению деталей изображения. Если атмосферные условия позволяют, можно поднимать увеличение выше этого значения, чтобы рассмотреть объект в более крупном масштабе. С этим увеличением обычно наблюдают детали на диске планет, Луны и Солнца.
                                </p>
                                <p>
                                <b>Большое увеличение</b> применяют при обзоре диска Луны и Солнца, наблюдении спутников планет, а также наблюдении крупных деталей на дисках планет.
                                </p>
                                <p>
                                <b>Проницающее увеличение</b> является типовым увеличением для наблюдения большинства объектов глубокого космоса (мелкие галактики, планетарные туманности и звездные скопления).
                                </p>
                                <p>
                                <b>Среднее увеличение</b> обычно применяют при наблюдениях туманностей и ярких галактик.
                                </p>
                                <p>
                                <b>Умеренное увеличение</b> обычно применяют при наблюдениях ярких и крупных объектов каталога Мессье.
                                </p>
                                <p>
                                <b>Равнозрачковое увеличение</b> является минимальным увеличением телескопа. При равнозрачковом увеличении достигается такой размер выходного зрачка, который соответствует максимальному зрачку человеческого глаза (обычно он равен 6мм). При меньших увеличениях размер выходного зрачка будет расти и часть света просто не попадет в зрачок глаза, поэтому нет смысла ставить увеличения ниже равнозрачкового.
                                </p>
                                <p>
                                <b>Относительное отверстие</b> определяется как отношение диаметра объектива к фокусному расстоянию телескопа. Относительное отверстие обычно выражается в виде дроби 1/K или f/K. Телескопы с относительным отверстием от f/4 до f/6 как правило называют светосильными. Такие телескопы предназначены и для визуальных наблюдений, и для астрономической фотографии. Телескопы, у которых относительное отверстие лежит в пределах от f/6 до f/15 больше подходят для визуальных наблюдений нежели для фотографии. На таких телескопах обычно проще получить большие увеличения.
                                </p>
                                <p>
                                <b>Разрешающая способность телескопа</b> - это угловой размер объектов и деталей на них, которые можно различить в этот телескоп при отличных условиях наблюдения.
                                </p>
                                <p>
                                <b>Предельная величина звезд</b>, которую можно увидеть в этот телескоп при отличных условиях наблюдения.
                                </p>
                                <p>
                                <b>Размер кратеров на Луне</b> - кратеры такого размера можно увидеть на Луне в этот телескоп при отличных условиях наблюдения.
                                </p>
                            </Col>
                        </Row>
                    </Col>
                </Row>
            </Form>)}
        />
    );
};