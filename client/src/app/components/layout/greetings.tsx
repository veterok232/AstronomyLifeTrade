import React from "react";
import { contextStore } from "../../infrastructure/stores/contextStore";
import { isConsumer, isManager, isStaff } from "../../infrastructure/services/auth/authService";
import { Col, Row } from "reactstrap";

interface Props {
    className?: string;
}

export const Greetings = (props: Props) => {
    return (<>
        <Row>
            <Col>
                <Row>
                    <Col>
                        <span className={props.className}>Здравствуйте, {contextStore.fullName}!</span>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        {isManager() &&
                            <span className="section-description">Менеджер</span>
                        }
                        {isStaff() &&
                            <span className="section-description">Администратор</span>
                        }
                        {isConsumer() &&
                            <span className="section-description">Покупатель</span>
                        }
                    </Col>
                </Row>
            </Col>
        </Row>
    </>
    );
};