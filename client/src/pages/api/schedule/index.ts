import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: {
      "content-type": "application/json",
      authorization: `bearer ${requestBody["token"]}`,
    },
    body: JSON.stringify({
      name: requestBody["name"],
      odontologistId: requestBody["odontologistId"],
      startDay: requestBody["startDay"],
      startTime: requestBody["startTime"],
      endDay: requestBody["endDay"],
      endTime: requestBody["endTime"],
    }),
    method: "POST",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/schedule`,
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
      "content-type": "application/json",
      authorization: `bearer ${requestBody["token"]}`,
    },
    body: JSON.stringify({
      id: requestBody["id"],
      name: requestBody["name"],
      odontologistId: requestBody["odontologistId"],
      startDay: requestBody["startDay"],
      startTime: requestBody["startTime"],
      endDay: requestBody["endDay"],
      endTime: requestBody["endTime"],
    }),
    method: "PUT",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/schedule`,
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
      "content-type": "application/json",
      authorization: `bearer ${requestBody["token"]}`,
    },
    method: "DELETE",
  };
  const id = requestBody["id"];
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/schedule?id=${id}`,
    options,
  );
  return new Response(body.ok ? JSON.stringify(await body.text()) : "", {
    status: body.status,
  });
};
