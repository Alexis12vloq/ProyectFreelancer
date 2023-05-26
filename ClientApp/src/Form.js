import { Container, Row } from "reactstrap"
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.min.js";
import { Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody } from 'reactstrap';
import React, { useState } from 'react';
const FormModal = () => {
    var imagen644 = ''
    const [disabledForm, setDisabledForm] = useState(true);
    function handleChange(event) {
        const archivoImagen = event.target.files[0];
        const lector = new FileReader();
        lector.onload = function (evento) {
            const imagen = new Image();
            imagen.src = evento.target.result;

            imagen.onload = function () {
                const canvas = document.createElement('canvas');
                canvas.width = imagen.width;
                canvas.height = imagen.height;

                const context = canvas.getContext('2d');
                context.drawImage(imagen, 0, 0);

                const imagenBase64 = canvas.toDataURL('image/jpeg');
                imagen644 = imagenBase64
            };
        };
        lector.readAsDataURL(archivoImagen);
    };
    const guardarDatos = async (e) => {
        e.preventDefault();
        console.log(e.target.title.value)

        var contacto = {
            Titulo: e.target.title.value,
            NombresCompletos: e.target.nombres.value,
            Direccion: e.target.direccion.value,
            Descripcion: e.target.descripcion.value,
            TipoDocumento: e.target.tipodocumento.value,
            NumeroDocumento: e.target.documento.value,
            TipoMoneda: e.target.tipodemoneda.value,
            MontoCobrar: e.target.montoacobrar.value,
            Logo: imagen644, PdfRecibo: 'PdfGenerate'
        }
        const response = await fetch("api/recibo/Guardar", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(contacto)
        });

        if (response.ok) {
            const pdfBlob = await response.blob();
            const url = window.URL.createObjectURL(pdfBlob);
            const link = document.createElement('a');
            link.href = url;
            link.download = "Recibo Frelancer.pdf";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            window.URL.revokeObjectURL(url);
        } else {
            console.error('Error:', response.statusText);
        }

    }
    return (
            <Modal isOpen={true}>
                <ModalHeader className="text-danger">Recibo Freelancer</ModalHeader>
                <ModalBody>
                    <Form onSubmit={guardarDatos}>
                        <FormGroup>
                            <Label for="title">Title Recibo</Label>
                            <Input type="text" name="title" id="title" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="nombres">Nombres Completos</Label>
                            <Input type="text" name="nombres" id="nombres" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="direccion">Direccion</Label>
                            <Input type="text" name="direccion" id="direccion" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="descripcion">Descripcion</Label>
                            <Input type="text" name="descripcion" id="descripcion" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="tipodocumento">Tipo de Documento</Label>
                            <Input type="select" name="tipodocumento" id="tipodocumento">
                                <option>PASAPORTE</option>
                                <option selected>DNI</option>
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Label for="documento">Documento</Label>
                            <Input type="number" name="documento" id="documento" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="tipodemoneda">Tipo De Moneda</Label>
                            <Input type="select" name="tipodemoneda" id="tipodemoneda">
                                <option>USD</option>
                                <option selected>Soles</option>
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Label for="montoacobrar">Monto A Cobrar</Label>
                            <Input type="number" name="montoacobrar" id="montoacobrar" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="imagen">Imagen de Logo</Label>
                            <Input onChange={handleChange} type="file" name="imagen" id="imagen" accept="image/jpeg" />
                        </FormGroup>
                        <Button color="primary" type="submit">Generar PDF</Button>
                    </Form>
                </ModalBody>
            </Modal>
    )
}
export default FormModal;