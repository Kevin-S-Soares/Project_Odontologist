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
        id: requestBody["id"],
        name: requestBody["name"],
        phone: requestBody["phone"],
        email: requestBody["email"]
      }),
      method: "PUT",
    };
    console.log(options);
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
      method: "DELETE",
    };
    const id = requestBody["id"];
    console.log(requestBody["token"])
    const body = await fetch(
      `${process.env.SERVER ?? ""}/api/v1/odontologist?id=${id}`,
      options,
    );
    return new Response(body.ok ? JSON.stringify(await body.text()) : "", {
      status: body.status,
    });
  };
