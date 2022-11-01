
import { Card, CardBody, CardText, CardTitle, Col, Row } from "reactstrap"


export const Banjoist = ({ banjoist }) => {

    return <>
        <Card key={banjoist.id} style={{ width: '18rem' }}>
            <Row >
                <Col >

                    <CardBody>
                        <CardTitle><strong>Name:</strong></CardTitle>
                        <CardText>
                            {banjoist?.name}
                        </CardText>
                        <CardTitle><strong>User Type:</strong></CardTitle>
                        <CardText>{banjoist?.userType?.name}</CardText>
                    </CardBody>
                </Col>
            </Row>
        </Card>
    </>
}