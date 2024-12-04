import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: { 
      "content-type": "application/json" ,
      authorization: `bearer ${requestBody["token"]}`
    },
    body: JSON.stringify({
      name: requestBody["name"],
      phone: requestBody["phone"],
      email: requestBody["email"]
    }),
    method: "POST",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/odontologist`,
    options,
  );
  return new Response(body.ok ? JSON.stringify(await body.text()) : "", {
    status: body.status,
  });
};

export const PUT: APIRoute = async ({ request }) => {
    const requestBody = await request.json();
    const options = {
      headers: { 
        "content-type": "application/json" ,
        authorization: `bearer ${requestBody["token"]}`
      },
      body: JSON.stringify({
        name: requestBody["name"],
        phone: requestBody["phone"],
        email: requestBody["email"]
      }),
      method: "PUT",
    };
    const body = await fetch(
      `${process.env.SERVER ?? ""}/api/v1/odontologist`,
      options,
    );
    return new Response(body.ok ? JSON.stringify(await body.text()) : "", {
      status: body.status,
    });
  };

  export const DELETE: APIRoute = async ({ request }) => {
    const requestBody = await request.json();
    const options = {
      headers: { 
        "content-type": "application/json" ,
        authorization: `bearer ${requestBody["token"]}`
      },
      body: JSON.stringify({
        name: requestBody["name"],
        phone: requestBody["phone"],
        email: requestBody["email"]
      }),
      method: "DELETE",
    };
    const body = await fetch(
      `${process.env.SERVER ?? ""}/api/v1/odontologist`,
      options,
    );
    return new Response(body.ok ? JSON.stringify(await body.text()) : "", {
      status: body.status,
    });
  };
