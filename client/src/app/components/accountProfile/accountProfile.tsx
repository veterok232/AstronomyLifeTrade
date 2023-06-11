import React, { useMemo, useState } from "react";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { Local } from "../localization/local";
import { Button, Col, Form, Row } from "reactstrap";
import { logOut } from "../../infrastructure/services/identityService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";
import arrayMutators from "final-form-arrays";
import { UserInfoModel } from "../../dataModels/accountProfile/userInfoModel";
import useAsyncEffect from "use-async-effect";
import { getUserInfo, saveUserAddress, saveUserInfo } from "../../api/accountProfile/accountProfileApi";
import { EmailFormControl } from "../common/controls/formControls/emailFormControl";
import { PhoneFormControl } from "../common/controls/formControls/maskedFormControls/phoneFormControl";
import { TextFormControl } from "../common/controls/formControls/textFormControl";
import { requiredNonWhitespace, required } from "../common/controls/validation/validators";
import { DateFormControl } from "../common/controls/formControls/dateFormControl";
import { RichSelectFormControl } from "../common/controls/formControls/richSelectFormControl";
import { LabeledValue } from "../common/controls/labeledValue";
import { Address } from "../../dataModels/address";
import { showNotificationIfInvalid } from "../common/controls/validation/formValidators";
import { parseISO } from "date-fns";
import { ListResult } from "../../dataModels/common/listResult";
import { OrderListItem } from "../../dataModels/orders/orderListItem";
import { FilterData, ListRequestHandler } from "../common/lists/listRequestHandler";
import { SortOrder } from "../../dataModels/enums/sortOrder";
import { Constants } from "../constants";
import { Sortable } from "../../dataModels/common/sortable";
import { Pageable } from "../../dataModels/common/pageable";
import { calculatePageNumber } from "../../utils/paginationUtils";
import { OrderCard } from "../orders/orders/orderCard";
import { NoData } from "../common/presentation/noData";
import { modalsStore } from "../../infrastructure/stores/modalsStore";
import { modalsTypes } from "../layout/modals/modalsTypes";
import { getUserOrders } from "../../api/orders/ordersApi";
import { PaginationControl } from "../common/controls/pagination/paginationControl";

const genderOptions: LabeledValue[] = [{
    label: "Мужской",
    value: "Male",
}, {
    label: "Женский",
    value: "Female",
}, {
    label: "Инфузория",
    value: "Other",
}];

const defaultSorting: Sortable = {
    sortBy: "OrderNumber",
    direction: SortOrder.Descending,
};

const defaultPaging: Pageable = {
    pageNumber: 1,
    pageSize: Constants.paging.ordersHistoryPageSize,
};

export const AccountProfilePage = () => {
    const [userInfo, setUserInfo] = useState<UserInfoModel>();
    const [orders, setOrders] = useState<ListResult<OrderListItem>>();

    async function loadOrders(request: FilterData<{}>) {
        setOrders(await getUserOrders(request));
    }

    const listHandler = useMemo(() =>
        new ListRequestHandler<{}>(
            {}, defaultPaging, defaultSorting, loadOrders), []);

    useAsyncEffect(async () => {
        await loadOrders(listHandler.getRequest());
    }, []);

    const pagesCount = calculatePageNumber(orders?.totalCount, Constants.paging.ordersHistoryPageSize);

    useAsyncEffect(async () => {
        const userInfo = await getUserInfo();
        setUserInfo({
            ...userInfo,
            birthday: userInfo.birthday && parseISO(userInfo.birthday.toString()),
        });
    }, []);

    const onSaveUserInfo = async (userInfo: UserInfoModel, valid: boolean) => {
        if (!valid) {
            showNotificationIfInvalid(valid);
            return;
        }

        await saveUserInfo({ ...userInfo });
    };

    const onSaveAddress = async (address: Address) => {
        await saveUserAddress(address);
    };

    const onLogout = async () => {
        await logOut();
        sharedHistory.push(getRoute(routeLinks.catalog.root));
    };

    const onShowOrderDetails = (orderId: string) => {
        modalsStore.openModal({
            modalType: modalsTypes.orderDetailsModal,
            modalProps: {
                orderId: orderId,
                fromOrdersHistory: true,
            },
            size: "xl",
        });
    };

    return (
        <FinalForm
        onSubmit={() => null}
        initialValues={userInfo}
        mutators={{...arrayMutators}}
        render={({ values, handleSubmit, valid }: FormRenderProps<UserInfoModel>) => (
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Col>
                        <h1 className="ui-page-header pt-2"><Local id="AccountProfile_Title" /></h1>
                    </Col>
                    <Col>
                        <Button onClick={onLogout} className="float-right">
                            <Local id="Logout" />
                        </Button>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="PersonalData" /></h1>
                        </Row>
                        <Row className="section-description">
                            <p>Здесь вы можете изменить свои личные данные. При изменении адреса электронной почты, адрес для входа в аккаунт также изменится на новый.</p>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <TextFormControl label={"FirstName"} name={"firstName"} placeholder="Имя"
                                    validator={requiredNonWhitespace} markRequired />
                            </Col>
                            <Col className="pl-0">
                                <TextFormControl label={"LastName"} name={"lastName"} placeholder="Фамилия"
                                    validator={requiredNonWhitespace} markRequired />
                            </Col>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <PhoneFormControl label={"Phone"} name={"phone"}
                                    validator={required} markRequired />
                            </Col>
                            <Col className="pl-0">
                                <EmailFormControl label={"Email"} name={"email"} placeholder="Адрес электронной почты"
                                    validator={required} markRequired />
                            </Col>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <DateFormControl label={"Birthday"} name={"birthday"} />
                            </Col>
                            <Col className="pl-0">
                                <RichSelectFormControl
                                    label={"Gender"}
                                    name={"gender"}
                                    clearable
                                    options={genderOptions} />
                            </Col>
                        </Row>
                        <Row className="mt-3">
                            <Col>
                                <Button className="float-right" onClick={() => onSaveUserInfo(values, valid)}>
                                    <Local id="SaveChanges" />
                                </Button>
                            </Col>
                        </Row>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="DeliveryAddress" /></h1>
                        </Row>
                        <Row className="section-description">
                            <p>Здесь вы можете указать адрес доставки товаров курьером и использовать его в дальнейшем при оформлении заказов.</p>
                        </Row>
                        <Row>
                            <Col className="pl-0">
                                <TextFormControl label={"City"} name={"address.city"} placeholder="Город" />
                                <TextFormControl label={"PostalCode"} name={"address.postalCode"} placeholder="Почтовый индекс" />
                            </Col>
                            <Col className="pl-0">
                                <TextFormControl label={"FullAddressLine"} name={"address.fullAddress"} placeholder="Улица, дом, квартира" />
                            </Col>
                        </Row>
                        <Row className="mt-3">
                            <Col>
                                <Button onClick={() => onSaveAddress(values.address)} className="float-right">
                                    <Local id="SaveChanges" />
                                </Button>
                            </Col>
                        </Row>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="mb-2">
                            <h1 className="ui-section-header pt-2"><Local id="OrdersHistory" /></h1>
                        </Row>
                        <Row className="p-3">
                            {orders?.totalCount > 0
                                ? orders.items.map((order, ind) =>
                                    <OrderCard
                                        key={ind}
                                        ind={ind}
                                        order={order}
                                        onShowOrderDetails={() => onShowOrderDetails(order.id)} />)
                                : <NoData localizationKey="NoOrdersYet"/>}
                        </Row>
                        {pagesCount > 1 &&
                            <div className="mt-3 justify-content-center">
                                <PaginationControl
                                    totalPages={pagesCount}
                                    onChange={x => listHandler.applyListOptions({ pageNumber: x })}
                                    extPageHandler={listHandler.pageHandler} />
                            </div>}
                    </Col>
                </Row>
            </Form>)}
        />
    );
};