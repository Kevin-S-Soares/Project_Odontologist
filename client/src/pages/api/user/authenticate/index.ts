import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: { "content-type": "application/json" },
    body: JSON.stringify({
      email: requestBody["email"],
      password: requestBody["password"],
    }),
    method: "POST",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/user/authenticate`,
    options,
  );
  return new Response(body.ok ? JSON.stringify(await body.text()) : "", {
    status: body.status,
  });
};
